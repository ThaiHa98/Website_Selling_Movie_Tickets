using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.SubtitleTable;
using Shared.DTOs.Theater;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly DBContext _dbContext;
        public TheaterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Response<Theater>> Create(Theater entity)
        {
            _dbContext.Theaters.Add(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<Theater>
                {
                    Success = true,
                    Data = entity,
                    Message = "Theater has been added successfully"
                };
            }
            else
            {
                return new Response<Theater>
                {
                    Success = false,
                    Message = "Failed to add theater"
                };
            }
        }

        public async Task<Response<Theater>> Delete(int id)
        {
            var entity = await _dbContext.Theaters.FindAsync(id);
            _dbContext.Theaters.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<Theater>
                {
                    Success = true,
                    Data = entity,
                    Message = "Theater has been added successfully"
                };
            }
            else
            {
                return new Response<Theater>
                {
                    Success = false,
                    Message = "Failed to add theater"
                };
            }
        }

        public async Task<List<TheaterModel>> GetAll()
        {
            // Lấy tất cả thông tin từ Theater
            var theaters = await _dbContext.Theaters
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    SubtitleTableIds = x.SubtitleTable_Id,
                    x.Date,
                    x.Address
                })
                .ToListAsync();
            if (theaters == null || !theaters.Any())
            {
                return new List<TheaterModel>();
            }
            // Tạo danh sách các TheaterModel để chứa kết quả
            var theaterModels = new List<TheaterModel>();

            foreach (var theater in theaters)
            {
                // Xử lý TheaterIds trong bộ nhớ
                var subtitleTableIds = theater.SubtitleTableIds.Split(',').Select(int.Parse).ToList();

                var subtitleTables = await _dbContext.SubtitleTables
                    .Where(x => subtitleTableIds.Contains(x.Id))
                    .Select(x => new Shared.DTOs.Theater.SubtitleTableModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TimeSlot_Id = x.TimeSlot_Id,
                    })
                    .ToListAsync();
                var theaterModel = new TheaterModel
                {
                    Id = theater.Id,
                    Name = theater.Name,
                    Address = theater.Address,
                    SubtitleTable = subtitleTables,
                    Date = theater.Date
                };
                theaterModels.Add(theaterModel);
            }
            return theaterModels;
        }

        public async Task<TheaterModel> GetById(int id)
        {
            // Lấy đối tượng Theater duy nhất với Id tương ứng
            var theater = await _dbContext.Theaters
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Address,
                    SubtitleTableIds = x.SubtitleTable_Id,
                    x.Date
                })
                .FirstOrDefaultAsync(); // Lấy đối tượng đầu tiên hoặc mặc định là null

            if (theater == null)
            {
                return null; // Trả về null nếu không tìm thấy
            }

            // Chuyển đổi SubtitleTableIds từ chuỗi sang danh sách các số nguyên
            var subtitleTableIds = theater.SubtitleTableIds.Split(',').Select(int.Parse).ToList();

            // Lấy danh sách SubtitleTable tương ứng
            var subtitleTables = await _dbContext.SubtitleTables
                .Where(x => subtitleTableIds.Contains(x.Id))
                .Select(x => new Shared.DTOs.Theater.SubtitleTableModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    TimeSlot_Id = x.TimeSlot_Id,
                })
                .ToListAsync();

            // Tạo đối tượng TheaterModel để trả về
            var theaterModel = new TheaterModel
            {
                Id = theater.Id,
                Name = theater.Name,
                Address = theater.Address,
                SubtitleTable = subtitleTables,
                Date = theater.Date
            };

            return theaterModel; // Trả về đối tượng TheaterModel
        }

        public async Task<Pagination<Theater>> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.Theaters.CountAsync();
            var items = await _dbContext.Theaters
                         .Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();

            return new Pagination<Theater>(pageIndex, pageSize, totalRecords, items);
        }

        public async Task<Response<Theater>> Update(Theater entity)
        {
            _dbContext.Theaters.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<Theater>
                {
                    Success = true,
                    Data = entity,
                    Message = "Theater has been added successfully"
                };
            }
            else
            {
                return new Response<Theater>
                {
                    Success = false,
                    Message = "Failed to add theater"
                };
            }
        }
    }
}
