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
                List<string> seatIds = request.Seat; // Nhận danh sách ghế từ yêu cầu

                var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.User_Id);
                if (user == null) throw new Exception("UserId not found");

                var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == request.Movies_Id);
                if (movie == null) throw new Exception("MovieId not found");

                var timeSlot = _dbContext.TimeSlots.FirstOrDefault(x => x.Id == request.TimeSlot_Id);
                if (timeSlot == null) throw new Exception("TimeSlotId not found");

                var chairType = _dbContext.ChairTypes.FirstOrDefault(x => x.Id == request.ChairType_Id);
                if (chairType == null) throw new Exception("ChairType_Id not found");

                var screeningRoom = _dbContext.ScreeningRooms.FirstOrDefault(x => x.Id == request.ScreeningRoom_Id);
                if (screeningRoom == null) throw new Exception("ScreeningRoom_Id not found");

                var theaters = _dbContext.Theaters.FirstOrDefault(x => x.Id == request.Theaters_Id);
                if (theaters == null) throw new Exception("Theaters_Id not found");

                var subtitleTable = _dbContext.SubtitleTables.FirstOrDefault(x => x.Id == request.SubtitleTable_Id);
                if (subtitleTable == null) throw new Exception("SubtitleTable_Id not found");

                var tickets = new List<Ticket>();

                // Kiểm tra số lượng ghế và đồ uống/bỏng ngô
                if (request.PopcornandDrinks_Id.Count != request.PopcornandDrinks_Quantity.Count ||
                    request.PopcornandDrinks_Id.Count != seatIds.Count)
                {
                    throw new Exception("Mismatched counts of seat IDs and popcorn/drink selections.");
                }

                for (int i = 0; i < seatIds.Count; i++)
                {
                    var seatId = seatIds[i];

                    // Tính giá vé cho ghế
                    decimal ticketPrice = CalculateTicketPrice(seatId);

                    // Tính tổng giá cho popcorn và drinks
                    decimal totalPopcornandDrinksPrice = 0;

                    // Lặp qua các loại đồ uống/bỏng ngô
                    for (int j = 0; j < request.PopcornandDrinks_Id.Count; j++)
                    {
                        var popcornandDrink = _dbContext.PopcornandDrinks.FirstOrDefault(x => x.Id == request.PopcornandDrinks_Id[j]);
                        if (popcornandDrink != null)
                        {
                            totalPopcornandDrinksPrice += popcornandDrink.Price * request.PopcornandDrinks_Quantity[j];
                        }
                    }

                    // Tạo vé
                    var ticket = new Ticket
                    {
                        Id = GenerateTicketId(),
                        User_Id = user.Id,
                        User_Name = user.Name,
                        Movies_Id = movie.Id,
                        TimeSlot_Id = timeSlot.Id,
                        Seat = seatId,
                        ChairType_Id = chairType.Id,
                        ScreeningRoom_Id = screeningRoom.Id,
                        Theaters_Id = theaters.Id,
                        SubtitleTable_Id = subtitleTable.Id,
                        PopcornandDrinks_Id = request.PopcornandDrinks_Id[i],
                        PopcornandDrinks_Quantity = request.PopcornandDrinks_Quantity[i],
                        ToatalPricePopcornandDrinks = totalPopcornandDrinksPrice,
                        ToatalPriceTicket = ticketPrice + totalPopcornandDrinksPrice, // Tổng tiền vé
                        Status = StatusTicket.Paid
                    };

                    tickets.Add(ticket);
                }

                await _ticketService.AddAsync(tickets);
                return new Response<Ticket>
                {
                    Data = tickets.FirstOrDefault(),
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
        private decimal CalculateTicketPrice(string seatId)
        {
            // Phân tích hàng và số ghế từ chuỗi seatId
            char row = seatId[0]; // Lấy ký tự đầu tiên (hàng ghế)
            int seatNumber;

            // Kiểm tra xem phần còn lại của chuỗi có phải là số không
            if (!int.TryParse(seatId.Substring(1), out seatNumber))
            {
                throw new Exception($"Invalid seat format: {seatId}");
            }

            // Lấy thông tin ghế từ cơ sở dữ liệu
            var seat = _dbContext.Seats.FirstOrDefault(x => x.Row == row.ToString() && x.Number == seatNumber.ToString());
            if (seat == null)
            {
                throw new Exception($"Seat {seatId} not found");
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

        public void IncreaseTheNumberOfPopcornandDrinks(string ticketId, int popcornandDrinkId, int increaseQuantity)
        {
            var ticket = _dbContext.Tickets.FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            // Tìm loại đồ uống/bỏng ngô
            var popcornandDrink = _dbContext.PopcornandDrinks.FirstOrDefault(x => x.Id == popcornandDrinkId);
            if (popcornandDrink == null)
            {
                throw new Exception("Popcorn/Drink not found");
            }

            // Cập nhật số lượng và tổng giá
            ticket.PopcornandDrinks_Quantity += increaseQuantity;
            ticket.ToatalPricePopcornandDrinks = popcornandDrink.Price * ticket.PopcornandDrinks_Quantity;

            // Cập nhật tổng giá vé
            ticket.ToatalPriceTicket = CalculateTicketPrice(ticket.Seat) + ticket.ToatalPricePopcornandDrinks;

            _dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        public void ReduceTheAmountOfPopcornandDrinks(string ticketId, int popcornandDrinkId, int reduceQuantity)
        {
            var ticket = _dbContext.Tickets.FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            // Tìm loại đồ uống/bỏng ngô
            var popcornandDrink = _dbContext.PopcornandDrinks.FirstOrDefault(x => x.Id == popcornandDrinkId);
            if (popcornandDrink == null)
            {
                throw new Exception("Popcorn/Drink not found");
            }

            // Kiểm tra xem số lượng có thể giảm không
            if (ticket.PopcornandDrinks_Quantity < reduceQuantity)
            {
                throw new Exception("Cannot reduce quantity more than available");
            }

            // Cập nhật số lượng và tổng giá
            ticket.PopcornandDrinks_Quantity -= reduceQuantity;
            ticket.ToatalPricePopcornandDrinks = popcornandDrink.Price * ticket.PopcornandDrinks_Quantity;

            // Cập nhật tổng giá vé
            ticket.ToatalPriceTicket = CalculateTicketPrice(ticket.Seat) + ticket.ToatalPricePopcornandDrinks;

            _dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        }
    }
}
