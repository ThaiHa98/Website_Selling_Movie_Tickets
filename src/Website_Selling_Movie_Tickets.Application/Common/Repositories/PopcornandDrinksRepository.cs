using Microsoft.EntityFrameworkCore;
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

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class PopcornandDrinksRepository : IPopcornandDrinksRepository
    {
        private readonly DBContext _dBContext;
        public PopcornandDrinksRepository(DBContext dbContext)
        {
            _dBContext = dbContext;
        }
        public async Task<Response<PopcornandDrinks>> AddAsync(PopcornandDrinks popcornandDrinks)
        {
            _dBContext.PopcornandDrinks.Add(popcornandDrinks);
            var result = await _dBContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<PopcornandDrinks>
                {
                    Success = true,
                    Data = popcornandDrinks,
                    Message = "PopcornandDrinks has been added successfully"
                };
            }
            else
            {
                return new Response<PopcornandDrinks>
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

        public async Task<Response<List<PopcornandDrinks>>> GetAllAsync()
        {
            var result = await _dBContext.PopcornandDrinks.ToListAsync();
            if (!result.Any())
            {
                return new Response<List<PopcornandDrinks>>
                {
                    Success = false,
                    Data = null,
                    Message = "No Popcorn and Drinks found."
                };
            }
            return new Response<List<PopcornandDrinks>>
            {
                Success = true,
                Data = result,
                Message = "Retrieved successfully."
            };
        }

        public async Task<Response<PopcornandDrinks>> GetById(int id)
        {
            var result = await _dBContext.PopcornandDrinks.FirstOrDefaultAsync(x => x.Id == id);
            if(result == null)
            {
                return new Response<PopcornandDrinks>
                {
                    Success = false,
                    Data = null,
                    Message = "Popcorn and Drink not found."
                };
            }
            return new Response<PopcornandDrinks>
            {
                Success = true,
                Data = result,
                Message = "Retrieved successfully."
            };
        }

        public async Task<Response<Pagination<PopcornandDrinks>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return new Response<Pagination<PopcornandDrinks>>
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

                var pagination = new Pagination<PopcornandDrinks>(pageIndex, pageSize, totalRecords, items);

                return new Response<Pagination<PopcornandDrinks>>
                {
                    Data = pagination,
                    Success = true,
                    Message = "Data retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<Pagination<PopcornandDrinks>>
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
            return "Update Successfully";
        }
    }
}
