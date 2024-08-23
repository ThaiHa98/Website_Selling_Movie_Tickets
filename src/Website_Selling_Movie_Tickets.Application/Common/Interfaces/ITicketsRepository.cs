using Shared.DTOs.Ticket;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface ITicketsRepository
    {
        Task<List<Ticket>> GetAllTickets();
        Task<Pagination<Ticket>> GetAllTicketsPagination(int pageIndex, int pageSize);
        Task<Ticket> GetTicketsById(string id);
        Task<Response<List<Ticket>>> AddAsync(List<Ticket> tickets);
        Task<Response<Ticket>> UpdateAsync (Ticket ticket);
        Task<Response<Ticket>> DeleteAsync (string id);
    }
}
