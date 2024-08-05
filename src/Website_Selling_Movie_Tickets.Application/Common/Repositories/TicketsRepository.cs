using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Ticket;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly DBContext _dbContext;
        public TicketsRepository(DBContext dbContext) 
        { 
            _dbContext = dbContext; 
        }
        public async Task<Response<TicketModel>> AddAsync(TicketModel ticketModel)
        {
            try
            {
                if (ticketModel == null)
                {
                    throw new ArgumentNullException(nameof(ticketModel), "The request was not fully entered into the data fields");
                }

                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == ticketModel.User_Id);
                if (user == null)
                {
                    throw new Exception("User Id not found");
                }

                var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == ticketModel.Movies_Id);
                if (movie == null)
                {
                    throw new Exception("Movie Id not found");
                }

                var timeSlot = await _dbContext.TimeSlots.FirstOrDefaultAsync(x => x.Id == ticketModel.TimeSlot_Id);
                if (timeSlot == null)
                {
                    throw new Exception("TimeSlot Id not found");
                }

                var screeningRoom = await _dbContext.ScreeningRooms.FirstOrDefaultAsync(x => x.Id == ticketModel.ScreeningRoom_Id);
                if (screeningRoom == null)
                {
                    throw new Exception("ScreeningRoom Id not found");
                }

                var theater = await _dbContext.Theaters.FirstOrDefaultAsync(x => x.Id == ticketModel.Theaters_Id);
                if (theater == null)
                {
                    throw new Exception("Theater Id not found");
                }

                // Lấy danh sách loại ghế
                var chairTypes = await _dbContext.ChairTypes
                    .Where(x => ticketModel.ChairType_Id.Contains(x.Id))
                    .ToListAsync();

                if (chairTypes.Count != ticketModel.ChairType_Id.Count)
                {
                    throw new Exception("One or more ChairType Ids not found");
                }

                // Thêm nhiều vé cho từng loại ghế
                foreach (var chairTypeId in ticketModel.ChairType_Id)
                {
                    var chairType = chairTypes.FirstOrDefault(x => x.Id == chairTypeId);
                    if (chairType == null)
                    {
                        throw new Exception($"ChairType Id {chairTypeId} not found");
                    }

                    var ticket = new Ticket
                    {
                        Id = GenerateTicketId(), // Gán ID mới
                        User_Id = user.Id,
                        User_Name = user.Name,
                        Movies_Id = movie.Id,
                        TimeSlot_Id = timeSlot.Id,
                        ChairType_Id = chairType.Id,
                        ChairType_Price = chairType.Price,
                        ScreeningRoom_Id = screeningRoom.Id,
                        Theaters_Id = theater.Id,
                        Status = StatusTicket.Open,
                    };

                    _dbContext.Tickets.Add(ticket);
                }

                var result = await _dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    var ticketsModel = new TicketModel
                    {
                        User_Id = user.Id,
                        Movies_Id = movie.Id,
                        TimeSlot_Id = timeSlot.Id,
                        ChairType_Id = ticketModel.ChairType_Id,
                        ScreeningRoom_Id = screeningRoom.Id,
                        Theaters_Id = theater.Id,
                    };

                    return new Response<TicketModel>
                    {
                        Data = ticketsModel,
                        Success = true,
                        Message = "Tickets have been added successfully"
                    };
                }
                else
                {
                    return new Response<TicketModel>
                    {
                        Success = false,
                        Message = "Failed to add tickets"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<TicketModel>
                {
                    Success = false,
                    Message = $"Failed to add tickets: {ex.Message}"
                };
            }
        }

        public async Task<Response<Ticket>> DeleteAsync(string id)
        {
            var ticket = await _dbContext.Tickets.FindAsync(id);
            _dbContext.Tickets.Remove(ticket);
            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return new Response<Ticket>
                {
                    Data = ticket,
                    Success = true,
                    Message = "Ticket has been deleted successfully"
                };
            }
            else
            {
                return new Response<Ticket>
                {
                    Success = false,
                    Message = "Failed to delete ticket"
                };
            }
        }

        public Task<List<Ticket>> GetAllTickets()
        {
            return _dbContext.Tickets.ToListAsync();
        }

        public async Task<Pagination<Ticket>> GetAllTicketsPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.Tickets.CountAsync();
            var items = _dbContext.Tickets
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new Pagination<Ticket>(pageIndex, pageSize, totalRecords, items);
        }

        public async Task<Ticket> GetTicketsById(string id)
        {
            var ticket = await _dbContext.Tickets.FindAsync(id);
            return ticket;
        }

        public async Task<Response<Ticket>> UpdateAsync(Ticket ticket)
        {
            _dbContext.Tickets.Update(ticket);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<Ticket>
                {
                    Data = ticket,
                    Success = true,
                    Message = "Ticket has been update successfully"
                };
            }
            else
            {
                return new Response<Ticket>
                {
                    Success = false,
                    Message = "Failed to update ticket"
                };
            }
        }
        //Tạo Id
        private string GenerateTicketId()
        {
            var lastTicket = _dbContext.Tickets
                .OrderByDescending(t => t.Id)
                .FirstOrDefault();

            int newId = lastTicket != null ? int.Parse(lastTicket.Id.Substring(1)) + 1 : 1;
            return $"I{newId:D6}"; // Đảm bảo ID luôn có 6 chữ số
        }
    }
}
