using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.MoviesView;
using Shared.DTOs.SearchStatusMovies;
using Shared.DTOs.Theater;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
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

        public async Task<MoviesViewModel> GetById(int id, DateTime premiere)
        {
            try
            {
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == id && x.Premiere == premiere)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Image,
                        x.GenreId,
                        x.RunningTime,
                        x.Premiere,
                        x.Language,
                        x.Rated,
                        x.Description,
                        x.Director,
                        x.Actors,
                        x.Status,
                        TheaterIds = x.TheatersIds
                    })
                    .FirstOrDefaultAsync();

                if (movie == null)
                {
                    return new MoviesViewModel
                    {
                        Id = id,
                        Name = "Movie not found",
                        Premiere = premiere
                    };
                }

                var genre = await _dbContext.Genres
                    .Where(g => g.Id == movie.GenreId)
                    .Select(g => new GenreViewModel
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .FirstOrDefaultAsync();

                var theaterViewModels = new List<TheaterViewModel>();
                var theaterIds = new List<int>();

                try
                {
                    if (!string.IsNullOrEmpty(movie.TheaterIds))
                    {
                        theaterIds = movie.TheaterIds.Split(' ')
                            .Select(id => int.TryParse(id.Trim(), out var parsedId) ? parsedId : (int?)null)
                            .Where(id => id.HasValue)
                            .Select(id => id.Value)
                            .ToList();
                    }
                }
                catch (FormatException ex)
                {
                    throw new ApplicationException("An error occurred while parsing theater IDs.", ex);
                }

                if (theaterIds.Any())
                {
                    theaterViewModels = await _dbContext.Theaters
                        .Where(x => theaterIds.Contains(x.Id))
                        .Select(x => new TheaterViewModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Address = x.Address,
                            SubtitleTable_Id = x.SubtitleTable_Id,
                            Date = x.Date
                        })
                        .ToListAsync();
                }

                var viewModel = new MoviesViewModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Image = movie.Image,
                    GenreId = genre?.Id ?? 0,
                    Genre_Name = genre?.Name ?? "Unknown",
                    RunningTime = movie.RunningTime,
                    Premiere = movie.Premiere,
                    Language = movie.Language,
                    Rated = movie.Rated,
                    Description = movie.Description,
                    Director = movie.Director,
                    Actors = movie.Actors,
                    Theaters = theaterViewModels
                };

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving movie details.", ex);
            }
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

        public async Task<List<MoviesViewModel>> SearchStatusAsync(string status)
        {
            try
            {
                // Chuyển đổi status từ chuỗi thành enum nếu cần
                if (!Enum.TryParse(status, true, out StatusMovie statusEnum))
                {
                    throw new ArgumentException("Invalid status value");
                }

                // Lấy tất cả các phim với trạng thái nhất định
                var movies = await _dbContext.Movies
                    .Where(x => x.Status == statusEnum)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Image,
                        x.GenreId,
                        x.RunningTime,
                        x.Premiere,
                        x.Language,
                        x.Rated,
                        x.Description,
                        x.Director,
                        x.Actors,
                        x.Status,
                        TheaterIds = x.TheatersIds
                    })
                    .ToListAsync();

                // Nếu không có phim nào với trạng thái này
                if (movies == null || !movies.Any())
                {
                    return new List<MoviesViewModel>(); // Trả về danh sách rỗng
                }

                // Danh sách kết quả
                var movieViewModels = new List<MoviesViewModel>();

                foreach (var movie in movies)
                {
                    var genre = await _dbContext.Genres
                        .Where(g => g.Id == movie.GenreId)
                        .Select(g => new GenreViewModel
                        {
                            Id = g.Id,
                            Name = g.Name
                        })
                        .FirstOrDefaultAsync();

                    var theaterViewModels = new List<TheaterViewModel>();
                    var theaterIds = new List<int>();

                    try
                    {
                        if (!string.IsNullOrEmpty(movie.TheaterIds))
                        {
                            theaterIds = movie.TheaterIds.Split(' ')
                                .Select(id => int.TryParse(id.Trim(), out var parsedId) ? parsedId : (int?)null)
                                .Where(id => id.HasValue)
                                .Select(id => id.Value)
                                .ToList();
                        }
                    }
                    catch (FormatException ex)
                    {
                        throw new ApplicationException("An error occurred while parsing theater IDs.", ex);
                    }

                    if (theaterIds.Any())
                    {
                        theaterViewModels = await _dbContext.Theaters
                            .Where(x => theaterIds.Contains(x.Id))
                            .Select(x => new TheaterViewModel
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Address = x.Address,
                                SubtitleTable_Id = x.SubtitleTable_Id,
                                Date = x.Date
                            })
                            .ToListAsync();
                    }

                    var viewModel = new MoviesViewModel
                    {
                        Id = movie.Id,
                        Name = movie.Name,
                        Image = movie.Image,
                        GenreId = genre?.Id ?? 0,
                        Genre_Name = genre?.Name ?? "Unknown",
                        RunningTime = movie.RunningTime,
                        Premiere = movie.Premiere,
                        Language = movie.Language,
                        Rated = movie.Rated,
                        Description = movie.Description,
                        Director = movie.Director,
                        Actors = movie.Actors,
                        Theaters = theaterViewModels
                    };

                    movieViewModels.Add(viewModel);
                }

                return movieViewModels;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving movie details.", ex);
            }
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
