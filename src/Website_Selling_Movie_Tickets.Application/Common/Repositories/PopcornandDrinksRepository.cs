using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.PopcornandDrinks;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class PopcornandDrinksRepository : IPopcornandDrinksRepository
    {
        private readonly DBContext _dBContext;
        private readonly IConfiguration _configuration;
        public PopcornandDrinksRepository(DBContext dbContext,IConfiguration configuration)
        {
            _dBContext = dbContext;
            _configuration = configuration;
        }
        public async Task<Response<PopcornandDrink>> AddAsync(PopcornandDrink popcornandDrinks)
        {
            _dBContext.PopcornandDrinks.Add(popcornandDrinks);
            var result = await _dBContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<PopcornandDrink>
                {
                    Success = true,
                    Data = popcornandDrinks,
                    Message = "PopcornandDrinks has been added successfully"
                };
            }
            else
            {
                return new Response<PopcornandDrink>
                {
                    Success = false,
                    Message = "Failed to add popcornandDrinks"
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dBContext.PopcornandDrinks.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }
            _dBContext.PopcornandDrinks.Remove(entity);
            var result = await _dBContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Response<List<PopcornandDrink>>> GetAllAsync()
        {
            var result = await _dBContext.PopcornandDrinks.ToListAsync();
            if (!result.Any())
            {
                return new Response<List<PopcornandDrink>>
                {
                    Success = false,
                    Data = null,
                    Message = "No Popcorn and Drinks found."
                };
            }
            return new Response<List<PopcornandDrink>>
            {
                Success = true,
                Data = result,
                Message = "Retrieved successfully."
            };
        }

        public async Task<Response<PopcornandDrink>> GetById(int id)
        {
            var result = await _dBContext.PopcornandDrinks.FirstOrDefaultAsync(x => x.Id == id);
            if(result == null)
            {
                return new Response<PopcornandDrink>
                {
                    Success = false,
                    Data = null,
                    Message = "Popcorn and Drink not found."
                };
            }
            return new Response<PopcornandDrink>
            {
                Success = true,
                Data = result,
                Message = "Retrieved successfully."
            };
        }

        public async Task<Response<Pagination<PopcornandDrink>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return new Response<Pagination<PopcornandDrink>>
                    {
                        Data = null,
                        Success = false,
                        Message = "Invalid pagination parameters."
                    };
                }

                var totalRecords = await _dBContext.PopcornandDrinks.CountAsync(cancellationToken);
                var items = await _dBContext.PopcornandDrinks
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                var pagination = new Pagination<PopcornandDrink>(pageIndex, pageSize, totalRecords, items);

                return new Response<Pagination<PopcornandDrink>>
                {
                    Data = pagination,
                    Success = true,
                    Message = "Data retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<Pagination<PopcornandDrink>>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occurred while retrieving data: {ex.Message}"
                };
            }
        }

        public async Task<string> UpdateAsync(PopcornandDrinksModel popcornandDrinksModel)
        {
            var query = await _dBContext.PopcornandDrinks.FirstOrDefaultAsync(x => x.Id ==  popcornandDrinksModel.Id);
            if(query == null)
            {
                throw new Exception("PopcornandDrinks_Id not found");
            }
            query.Id = popcornandDrinksModel.Id;
            query.Price = popcornandDrinksModel.Price;
            await _dBContext.SaveChangesAsync();
            return "Update Successfully";
        }

        public async Task<byte[]> GetPopcornandDrinkImageBytes(int id)
        {
            try
            {
                var popcornandDrinks = await _dBContext.PopcornandDrinks
                    .Where(x => x.Id == id)
                    .Select(x => new
                    {
                        x.Image
                    })
                    .FirstOrDefaultAsync();

                if (popcornandDrinks == null)
                {
                    throw new Exception($"Không tìm thấy bộ phim nào với ID {id}");
                }

                var baseFolder = _configuration.GetValue<string>("BaseAddress");

                if (string.IsNullOrEmpty(baseFolder))
                {
                    throw new Exception("Cấu hình BaseAddress không hợp lệ hoặc thiếu.");
                }

                var baseFolderLocal = new Uri(baseFolder).LocalPath;
                var completeFilePath = Path.Combine(baseFolderLocal, popcornandDrinks.Image);

                if (!System.IO.File.Exists(completeFilePath))
                {
                    throw new Exception($"Không tìm thấy tệp tại đường dẫn: {completeFilePath}");
                }

                return await System.IO.File.ReadAllBytesAsync(completeFilePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi lấy thông tin hình ảnh của bộ phim.", ex);
            }
        }
    }
}
