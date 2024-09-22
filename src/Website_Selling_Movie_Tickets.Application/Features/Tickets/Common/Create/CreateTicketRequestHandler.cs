using MediatR;
using Shared.DTOs.Ticket;
using Shared.SeedWork;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Create
{
    public class CreateTicketRequestHandler : IRequestHandler<CreateTicketRequest, Response<Ticket>>
    {
        private readonly ITicketsRepository _ticketService;
        private readonly DBContext _dbContext;

        public CreateTicketRequestHandler(ITicketsRepository ticketService, DBContext dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
        }

        public async Task<Response<Ticket>> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<int> seatIds = request.Seat_Id;

                var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.User_Id);
                if (user == null)
                {
                    throw new Exception("UserId not found");
                }
                var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == request.Movies_Id);
                if (movie == null)
                {
                    throw new Exception("MovieId not found");
                }
                var timeSlot = _dbContext.TimeSlots.FirstOrDefault(x => x.Id == request.TimeSlot_Id);
                if (timeSlot == null)
                {
                    throw new Exception("TimeSlotId not found");
                }
                var chairType = _dbContext.ChairTypes.FirstOrDefault(x => x.Id == request.ChairType_Id);
                if (chairType == null)
                {
                    throw new Exception("ChairType_Id not found");
                }
                var screeningRoom = _dbContext.ScreeningRooms.FirstOrDefault(x => x.Id == request.ScreeningRoom_Id);
                if (screeningRoom == null)
                {
                    throw new Exception("ScreeningRoom_Id not found");
                }
                var theaters = _dbContext.Theaters.FirstOrDefault(x => x.Id == request.Theaters_Id);
                if (theaters == null)
                {
                    throw new Exception("Theaters_Id not found");
                }
                var subtitleTable = _dbContext.SubtitleTables.FirstOrDefault(x => x.Id == request.SubtitleTable_Id);
                if (subtitleTable == null)
                {
                    throw new Exception("SubtitleTable_Id not found");
                }
                var popcornandDrinks = _dbContext.PopcornandDrinks.FirstOrDefault(x => x.Id == request.PopcornandDrinks_Id);
                if(popcornandDrinks == null)
                {
                    throw new Exception("popcornandDrinks_Id not found");
                }

                var tickets = new List<Ticket>();
                foreach (var seatId in seatIds)
                {
                    var seat = _dbContext.Seats.FirstOrDefault(x => x.Id == seatId);
                    if (seat == null)
                    {
                        throw new Exception($"Seat_Id {seatId} not found");
                    }

                    var ticket = new Ticket
                    {
                        Id = GenerateTicketId(),
                        User_Id = user.Id,
                        User_Name = user.Name,
                        Movies_Id = movie.Id,
                        TimeSlot_Id = timeSlot.Id,
                        Seat_Id = seat.Id,
                        ChairType_Id = chairType.Id,
                        ToatalPrice = CalculateTicketPrice(seatId),
                        ScreeningRoom_Id = screeningRoom.Id,
                        Theaters_Id = theaters.Id,
                        SeatNumber = seat.Number,
                        SubtitleTable_Id = subtitleTable.Id,
                        PopcornandDrinks_Id = popcornandDrinks.Id,
                        PopcornandDrinks_Quantity = popcornandDrinks.Quantity,
                        Status = StatusTicket.Paid
                    };

                    tickets.Add(ticket);
                }

                // Lưu tất cả các vé
                await _ticketService.AddAsync(tickets);

                return new Response<Ticket>
                {
                    Data = tickets.FirstOrDefault(), // Trả về vé đầu tiên hoặc điều chỉnh nếu cần
                    Success = true
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating Tickets", ex);
            }
        }
        // Tạo Id
        private string GenerateTicketId()
        {
            var lastTicket = _dbContext.Tickets
                .OrderByDescending(t => t.Id)
                .FirstOrDefault();

            int newId = lastTicket != null ? int.Parse(lastTicket.Id.Substring(1)) + 1 : 1;
            return $"I{newId:D6}"; // Đảm bảo ID luôn có 6 chữ số
        }

        //Tính toán giá vé
        private decimal CalculateTicketPrice(int seatId)
        {
            // Lấy thông tin ghế từ cơ sở dữ liệu
            var seat = _dbContext.Seats.FirstOrDefault(x => x.Id == seatId);
            if (seat == null)
            {
                throw new Exception($"Seat_Id {seatId} not found");
            }

            // Lấy thông tin loại ghế từ cơ sở dữ liệu
            var chairType = _dbContext.ChairTypes.FirstOrDefault(x => x.Id == seat.ChairType_Id);
            if (chairType == null)
            {
                throw new Exception($"ChairType_Id {seat.ChairType_Id} not found");
            }

            // Trả về giá của loại ghế
            return chairType.Price;
        }
    }
}
