using Microsoft.EntityFrameworkCore;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly DBContext _dbContext;
        public TimeSlotRepository(DBContext dbContext) 
        { 
            _dbContext = dbContext; 
        }
        public async Task<Response<TimeSlot>> AddAsync(TimeSlot timeSlot)
        {
            _dbContext.TimeSlots.Add(timeSlot);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<TimeSlot>
                {
                    Success = true,
                    Data = timeSlot,
                    Message = "TimeSlot has been added successfully"
                };
            }
            else
            {
                return new Response<TimeSlot>
                {
                    Success = false,
                    Message = "Failed to add TimeSlot"
                };
            }
        }

        public async Task<List<TimeSlot>> GetAll()
        {
            return await _dbContext.TimeSlots.ToListAsync();
        }

        public async Task<Pagination<TimeSlot>> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.TimeSlots.CountAsync();
            var items = await _dbContext.TimeSlots
                         .Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();

            return new Pagination<TimeSlot>(pageIndex, pageSize, totalRecords, items);
        }

        public async Task<TimeSlot> GetById(int id)
        {
            return await _dbContext.TimeSlots.FindAsync(id);
        }

        public async Task<Response<TimeSlot>> RemoveAsync(int id)
        {
            var entity = await _dbContext.TimeSlots.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Theater not found");
            }
            _dbContext.TimeSlots.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<TimeSlot>
                {
                    Success = true,
                    Data = entity,
                    Message = "TimeSlot has been remove successfully"
                };
            }
            else
            {
                return new Response<TimeSlot>
                {
                    Success = false,
                    Message = "Failed to remove timeslot"
                };
            }
        }

        public async Task<Response<string>> UpdateAsync(TimeSlot timeSlot)
        {
            _dbContext.TimeSlots.Update(timeSlot);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<string>
                {
                    Message = "TimeSlot has been update successfully"
                };
            }
            else
            {
                return new Response<string>
                {
                    Message = "Failed to update timeslot"
                };
            }
        }
    }
}
