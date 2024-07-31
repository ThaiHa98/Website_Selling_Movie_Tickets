﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<Theater>> GetAll()
        {
            return await _dbContext.Theaters.ToListAsync();
        }

        public async Task<Theater> GetById(int id)
        {
            return await _dbContext.Theaters.FindAsync(id);
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
