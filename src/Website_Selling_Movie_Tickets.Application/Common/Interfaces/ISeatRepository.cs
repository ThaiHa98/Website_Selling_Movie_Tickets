using Shared.DTOs.Seat;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface ISeatRepository
    {
        Task<List<SeatModel>> GetAll();
        Task<Pagination<Seat>> GetPagination(int pageIndex, int pageSize);
        Task<SeatModel> GetSeatById(int seatId);
        Task<Response<Seat>> AddAsync(Seat seat);
        Task<string> UpdateAsync (int Id);
        Task<bool> DeleteAsync (int Id);
    }
}
