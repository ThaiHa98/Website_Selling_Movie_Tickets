using MediatR;
using Shared.DTOs.Ticket;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
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
                List<string> seatIds = request.Seat;

                var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.User_Id);
                if (user == null)
                {
                    throw new Exception("UserId không tồn tại");
                }
                var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == request.Movies_Id);
                if (movie == null)
                {
                    throw new Exception("MovieId không tồn tại");
                }
                var timeSlot = _dbContext.TimeSlots.FirstOrDefault(x => x.Id == request.TimeSlot_Id);
                if (timeSlot == null)
                {
                    throw new Exception("TimeSlotId không tồn tại");
                }
                var chairType = _dbContext.ChairTypes.FirstOrDefault(x => x.Id == request.ChairType_Id);
                if (chairType == null)
                {
                    throw new Exception("ChairType_Id không tồn tại");
                }
                var screeningRoom = _dbContext.ScreeningRooms.FirstOrDefault(x => x.Id == request.ScreeningRoom_Id);
                if (screeningRoom == null)
                {
                    throw new Exception("ScreeningRoom_Id không tồn tại");
                }
                var theaters = _dbContext.Theaters.FirstOrDefault(x => x.Id == request.Theaters_Id);
                if (theaters == null)
                {
                    throw new Exception("Theaters_Id không tồn tại");
                }
                var subtitleTable = _dbContext.SubtitleTables.FirstOrDefault(x => x.Id == request.SubtitleTable_Id);
                if (subtitleTable == null)
                {
                    throw new Exception("SubtitleTable_Id không tồn tại");
                }

                var tickets = new List<Ticket>();
                decimal totalPrice = 0; // Tổng giá vé

                foreach (var seatId in seatIds)
                {
                    // Tách chuỗi ghế thành Row và Number
                    string row = seatId[0].ToString(); // Hàng là ký tự đầu tiên
                    int seatNumber = int.Parse(seatId.Substring(1)); // Số ghế là phần còn lại

                    // Tìm tất cả các ghế thuộc hàng đó
                    var seatsInRow = _dbContext.Seats
                        .Where(x => x.Row == row && x.ScreeningRoom_Id == request.ScreeningRoom_Id)
                        .ToList();

                    if (seatsInRow.Count == 0)
                    {
                        throw new Exception($"Row {row} không tồn tại");
                    }

                    // Kiểm tra xem số ghế có nằm trong khoảng không
                    bool seatExists = seatsInRow.Any(seat =>
                    {
                        var seatRange = seat.Number.Split('-');
                        int startSeat = int.Parse(seatRange[0]);
                        int endSeat = int.Parse(seatRange[1]);

                        return seatNumber >= startSeat && seatNumber <= endSeat;
                    });

                    if (!seatExists)
                    {
                        throw new Exception($"Seat_Id {seatId} không tồn tại trong hàng {row}");
                    }

                    // Tính giá vé cho ghế
                    var seat = seatsInRow.First(); // Lấy ghế đầu tiên để tính giá, điều chỉnh nếu cần
                    decimal seatPrice = CalculateTicketPrice(seat);
                    totalPrice += seatPrice;

                    // Tạo ticket
                    var ticket = new Ticket
                    {
                        Id = GenerateTicketId(),
                        User_Id = user.Id,
                        User_Name = user.Name,
                        Movies_Id = movie.Id,
                        TimeSlot_Id = timeSlot.Id,
                        Seat = $"{row}{seatNumber}",
                        ChairType_Id = seat.ChairType_Id,
                        ToatalPriceTicket = seatPrice,
                        ScreeningRoom_Id = screeningRoom.Id,
                        Theaters_Id = theaters.Id,
                        SeatNumber = seatNumber.ToString(),
                        SubtitleTable_Id = subtitleTable.Id,
                        Status = StatusTicket.Paid,
                        ShowTime = request.ShowTime
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
                throw new ApplicationException("Đã xảy ra lỗi trong quá trình tạo vé", ex);
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

        // Tính toán giá vé
        private decimal CalculateTicketPrice(Seat seat)
        {
            var chairType = _dbContext.ChairTypes.FirstOrDefault(x => x.Id == seat.ChairType_Id);
            if (chairType == null)
            {
                throw new Exception($"ChairType_Id {seat.ChairType_Id} not found");
            }

            return chairType.Price;
        }
    }
}
