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
    public class ChairTypeRepository : IChairTypeRepository
    {
        private readonly DBContext _dbContext;
        public ChairTypeRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Response<ChairType>> AddAsync(ChairType chairType)
        {
            _dbContext.ChairTypes.Add(chairType);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<ChairType>
                {
                    Success = true,
                    Data = chairType,
                    Message = "ChairType has been added successfully."
                };
            }
            else
            {
                return new Response<ChairType>
                {
                    Success = false,
                    Message = "An error occurred while adding the chairType."
                };
            }
        }

        public async Task<Response<ChairType>> DeleteAsync(int id)
        {
            var chairType = await _dbContext.ChairTypes.FindAsync(id);
            if (chairType == null) 
            {
                return null;
            }
            _dbContext.ChairTypes.Remove(chairType);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0) 
            {
                return new Response<ChairType>
                {
                    Data = chairType,
                    Success = true,
                    Message = "ChairType has been Delete successfully"
                };
            }
            else
            {
                return new Response<ChairType>
                {
                    Success = true,
                    Message = "An error occurred while delete the chairtype"
                };
            }
        }

        public async Task<Response<List<ChairType>>> GetAll()
        {
            try
            {
                var chairTypes = await _dbContext.ChairTypes.ToListAsync();
                if (chairTypes == null || chairTypes.Count == 0)
                {
                    return new Response<List<ChairType>>
                    {
                        Data = new List<ChairType>(),
                        Success = false,
                        Message = "No chair types found."
                    };
                }

                return new Response<List<ChairType>>
                {
                    Data = chairTypes,
                    Success = true,
                    Message = "Chair types retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<List<ChairType>>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occurred while retrieving chair types: {ex.Message}"
                };
            }
        }

        public async Task<ChairType> GetById(int id)
        {
            var chairType = await _dbContext.ChairTypes.FirstOrDefaultAsync(x => x.Id == id);
            return chairType;
        }

        public async Task<Response<Pagination<ChairType>>> GetPagination(int pageIndex, int pageSize)
        {
            try
            {
                var totalRecords = await _dbContext.ChairTypes.CountAsync();
                var items = await _dbContext.ChairTypes
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var pagination = new Pagination<ChairType>(pageIndex, pageSize, totalRecords, items);

                return new Response<Pagination<ChairType>>
                {
                    Data = pagination,
                    Success = true,
                    Message = "Data retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<Pagination<ChairType>>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occurred while retrieving data: {ex.Message}"
                };
            }
        }

        public async Task<Response<ChairType>> UpdateAsync(ChairType chairType)
        {
            try
            {
                _dbContext.ChairTypes.Update(chairType);
                var result = await _dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return new Response<ChairType>
                    {
                        Data = chairType,
                        Success = true,
                        Message = "ChairType has been updated successfully."
                    };
                }
                else
                {
                    return new Response<ChairType>
                    {
                        Data = chairType,
                        Success = false,
                        Message = "No changes were made to the ChairType."
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<ChairType>
                {
                    Success = false,
                    Message = $"An error occurred while updating the ChairType: {ex.Message}"
                };
            }
        }
    }
}
