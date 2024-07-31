using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<List<TimeSlot>> GetAll();
        Task<Pagination<TimeSlot>> GetPagination(int pageIndex, int pageSize);
        Task<TimeSlot> GetById(int id);
        Task<Response<TimeSlot>> AddAsync(TimeSlot timeSlot);
        Task<Response<string>> UpdateAsync(TimeSlot timeSlot);
        Task<Response<TimeSlot>> RemoveAsync(int id);
    }
}
