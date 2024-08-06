using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly DBContext _dbContext;
        private readonly IConfiguration _configuration;
        public MoviesRepository(DBContext dbContext, IConfiguration configuration) 
        {
            _configuration = configuration;
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

        public async Task<byte[]> GetMovieImageBytes(int id)
        {
            try
            {
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == id)
                    .Select(x => new
                    {
                        x.Image
                    })
                    .FirstOrDefaultAsync();

                if (movie == null)
                {
                    throw new Exception($"Không tìm thấy bộ phim nào với ID {id}");
                }

                var baseFolder = _configuration.GetValue<string>("BaseAddress");

                if (string.IsNullOrEmpty(baseFolder))
                {
                    throw new Exception("Cấu hình BaseAddress không hợp lệ hoặc thiếu.");
                }

                var baseFolderLocal = new Uri(baseFolder).LocalPath;
                var completeFilePath = Path.Combine(baseFolderLocal, movie.Image);

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


        public async Task<Pagination<Movie>> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.Movies.CountAsync();
            var items = await _dbContext.Movies
                                  .Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
            return new Pagination<Movie>(pageIndex, totalRecords,totalRecords, items);
        }

        public async Task<Movie> SearchByKeyAsync(string key)
        {
            var query = _dbContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(m => m.Name.Contains(key));
            }

            return await query.FirstOrDefaultAsync();
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
