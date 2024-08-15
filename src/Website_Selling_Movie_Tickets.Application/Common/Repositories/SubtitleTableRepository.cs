using Microsoft.EntityFrameworkCore;
using Shared.DTOs.SubtitleTable;
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
    public class SubtitleTableRepository : ISubtitleTableRepository
    {
        private readonly DBContext _dbContext;
        public SubtitleTableRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Response<SubtitleTable>> Create(SubtitleTable subtitleTable)
        {
            _dbContext.SubtitleTables.Add(subtitleTable);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<SubtitleTable>
                {
                    Success = true,
                    Data = subtitleTable,
                    Message = "TimeSlot has been added successfully"
                };
            }
            else
            {
                return new Response<SubtitleTable>
                {
                    Success = false,
                    Message = "Failed to add TimeSlot"
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.SubtitleTables.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dbContext.SubtitleTables.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<SubtitleTableModel>> GetAll()
        {
            // Lấy tất cả thông tin từ SubtitleTable
            var subtitleTables = await _dbContext.SubtitleTables
                .Select(st => new
                {
                    st.Id,
                    st.Name,
                    TimeSlotIds = st.TimeSlot_Id // Lấy TimeSlot_Id dưới dạng chuỗi
                })
                .ToListAsync();

            if (subtitleTables == null || !subtitleTables.Any())
            {
                return new List<SubtitleTableModel>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }

            // Tạo danh sách các SubtitleTableModel để chứa kết quả
            var subtitleTableModels = new List<SubtitleTableModel>();

            foreach (var subtitleTable in subtitleTables)
            {
                // Xử lý TimeSlotIds trong bộ nhớ
                var timeSlotIds = subtitleTable.TimeSlotIds.Split(' ').Select(int.Parse).ToList();

                // Lấy thông tin TimeSlot dựa trên các TimeSlot_Id
                var timeSlots = await _dbContext.TimeSlots
                    .Where(ts => timeSlotIds.Contains(ts.Id))
                    .Select(ts => new TimeSlotDetails
                    {
                        Id = ts.Id,
                        StartTime = ts.StartTime,
                        EndTime = ts.EndTime,
                        Date = ts.Date
                    })
                    .ToListAsync();

                // Tạo đối tượng SubtitleTableModel và thêm vào danh sách kết quả
                var subtitleTableModel = new SubtitleTableModel
                {
                    Id = subtitleTable.Id,
                    Name = subtitleTable.Name,
                    TimeSlots = timeSlots
                };

                subtitleTableModels.Add(subtitleTableModel);
            }

            return subtitleTableModels;
        }

        public async Task<SubtitleTableModel> GetById(int id)
        {
            // Lấy thông tin từ SubtitleTable
            var subtitleTable = await _dbContext.SubtitleTables
                .Where(st => st.Id == id)
                .Select(st => new
                {
                    st.Id,
                    st.Name,
                    TimeSlotIds = st.TimeSlot_Id // Lấy TimeSlot_Id dưới dạng chuỗi
                })
                .FirstOrDefaultAsync();

            if (subtitleTable == null)
            {
                return null; // Hoặc xử lý lỗi nếu không tìm thấy
            }

            // Xử lý TimeSlotIds trong bộ nhớ
            var timeSlotIds = subtitleTable.TimeSlotIds.Split(' ').Select(int.Parse).ToList();

            // Lấy thông tin TimeSlot dựa trên các TimeSlot_Id
            var timeSlots = await _dbContext.TimeSlots
                .Where(ts => timeSlotIds.Contains(ts.Id))
                .Select(ts => new TimeSlotDetails
                {
                    Id = ts.Id,
                    StartTime = ts.StartTime,
                    EndTime = ts.EndTime,
                    Date = ts.Date
                })
                .ToListAsync();

            // Tạo đối tượng SubtitleTableModel
            var result = new SubtitleTableModel
            {
                Id = subtitleTable.Id,
                Name = subtitleTable.Name,
                TimeSlots = timeSlots
            };

            return result;
        }

        public async Task<Pagination<SubtitleTable>> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.SubtitleTables.CountAsync();
            var items = await _dbContext.SubtitleTables
                         .Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
            return new Pagination<SubtitleTable>(pageIndex, pageSize, totalRecords, items);
        }

        public Task<SubtitleTable> SearchByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<SubtitleTable>> SearchStatusAsync(string status)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Update(SubtitleTable subtitleTable)
        {
            _dbContext.Update(subtitleTable);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return "Update Successfully";
            }
            else
            {
                return "Update Failed";
            }
        }
    }
}
