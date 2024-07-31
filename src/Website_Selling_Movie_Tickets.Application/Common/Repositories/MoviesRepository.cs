using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class MoviesRepository : IMoviesRepository
    {
        private readonly DBContext _dbContext;
        public MoviesRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Response<Movie>> Create(Movie movie)
        {
            await _dbContext.Movies.AddAsync(movie);
            var result = _dbContext.SaveChanges();
            if (result > 0)
            {
                return new Response<Movie>
                {
                    Success = true,
                    Data = movie,
                    Message = "Movie has been added successfully."
                };
            }
            else
            {
                return new Response<Movie>
                {
                    Success = false,
                    Message = "An error occurred while adding the movie."
                };
            }
        }

        public async Task<bool> Delete(Movie movie)
        {
            _dbContext.Remove(movie);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pagination<Movie>> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.Movies.CountAsync();
            var items = await _dbContext.Movies
                                  .Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
            return new Pagination<Movie>(pageIndex, totalRecords,totalRecords, items);
        }

        public async Task<string> Update(Movie movie)
        {
            _dbContext.Update(movie);
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
