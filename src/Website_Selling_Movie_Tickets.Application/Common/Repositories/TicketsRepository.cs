using AutoMapper; // Thêm namespace cho AutoMapper
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
        private readonly IMapper _mapper; // Khai báo IMapper

        public TicketsRepository(DBContext dbContext, IMapper mapper) // Inject IMapper
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<List<Ticket>>> AddAsync(List<Ticket> tickets)
        {
            try
            {
                _dbContext.Tickets.AddRange(tickets); // Add the entire list of tickets
                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<List<Ticket>>
                    {
                        Data = tickets,
                        Success = true,
                        Message = "Create Tickets Successfully"
                    };
                }
                else
                {
                    return new Response<List<Ticket>>
                    {
                        Success = false,
                        Message = "Failed to create tickets: No changes were saved to the database."
                    };
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating tickets. Details: " + ex.Message, ex);
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
    }
}
