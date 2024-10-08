﻿using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Booking;
using Shared.DTOs.MoviesView;
using Shared.DTOs.SearchStatusMovies;
using Shared.DTOs.SubtitleTables;
using Shared.DTOs.SubtitleTableTimeSlots;
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

        public async Task<List<TheaterViewModel>> GetPremiere(int id, DateTime premiere)
        {
            try
            {
                // Lấy thông tin bộ phim dựa trên id và premiere
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == id && x.Premiere == premiere)
                    .Select(x => new
                    {
                        TheaterIds = x.TheatersIds // Lấy danh sách TheaterIds
                    })
                    .FirstOrDefaultAsync();

                // Nếu không tìm thấy phim thì trả về danh sách rỗng
                if (movie == null || string.IsNullOrEmpty(movie.TheaterIds))
                {
                    return new List<TheaterViewModel>();
                }

                // Xử lý TheaterIds
                var theaterIds = movie.TheaterIds
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.TryParse(id, out var parsedId) ? (int?)parsedId : null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value)
                    .ToList();

                // Truy vấn danh sách các rạp chiếu dựa trên TheaterIds
                var theaterViewModels = await _dbContext.Theaters
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

                // Xử lý và truy vấn các SubtitleTable_Id
                foreach (var theater in theaterViewModels)
                {
                    var subtitleIds = theater.SubtitleTable_Id
                        .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(id => int.TryParse(id, out var parsedId) ? (int?)parsedId : null)
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();

                    var subtitleTables = await _dbContext.SubtitleTables
                        .Where(st => subtitleIds.Contains(st.Id))
                        .Select(st => new Shared.DTOs.MoviesView.SubtitleTableModel
                        {
                            Id = st.Id,
                            Name = st.Name,
                            TimeSlot_Id = st.TimeSlot_Id
                        })
                        .ToListAsync();

                    theater.SubtitleTable = subtitleTables;
                }

                return theaterViewModels;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving theaters.", ex);
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
            return new Pagination<Movie>(pageIndex, totalRecords, totalRecords, items);
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
                if (!Enum.TryParse<StatusMovie>(status, out var statusEnum))
                {
                    throw new ArgumentException("Invalid status value");
                }
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

                    if (!string.IsNullOrEmpty(movie.TheaterIds))
                    {
                        var theaterIds = movie.TheaterIds
                            .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(id =>
                            {
                                return int.TryParse(id, out var parsedId) ? parsedId : (int?)null;
                            })
                            .Where(id => id.HasValue)
                            .Select(id => id.Value)
                            .ToList();

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

        public async Task<List<MoviesViewModel>> SearchIronfilmreleased(string status)
        {
            try
            {
                if (!Enum.TryParse<StatusMovie>(status, out var statusEnum))
                {
                    throw new ArgumentException("Invalid status value");
                }
                var movies = await _dbContext.Movies
                    .Where(x => x.Status == statusEnum)
                    .Take(3)
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

                    if (!string.IsNullOrEmpty(movie.TheaterIds))
                    {
                        var theaterIds = movie.TheaterIds
                            .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(id =>
                            {
                                return int.TryParse(id, out var parsedId) ? parsedId : (int?)null;
                            })
                            .Where(id => id.HasValue)
                            .Select(id => id.Value)
                            .ToList();

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

        public async Task<MoviesViewModel> MoviesDetails(int id)
        {
            try
            {
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == id)
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
                        Name = "Movie not founf"
                    };
                }

                var genre = await _dbContext.Genres
                    .Where(g => g.Id == movie.GenreId)
                    .Select(g => new GenreViewModel
                    {
                        Id = g.Id,
                        Name = g.Name,
                    })
                    .FirstOrDefaultAsync();

                var theaterViewModels = new List<TheaterViewModel>();

                if (!string.IsNullOrEmpty(movie.TheaterIds))
                {
                    var theaterIds = movie.TheaterIds
                        .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(id =>
                        {
                            return int.TryParse(id, out var parsedId) ? parsedId : (int?)null;
                        })
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();
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
                                Date = x.Date,
                            })
                            .ToListAsync();
                    }
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

        public async Task<List<string>> GetTheaterAddressesByMovieId(int id)
        {
            try
            {
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == id)
                    .Select(x => new
                    {
                        x.TheatersIds
                    })
                    .FirstOrDefaultAsync();

                if (movie == null || string.IsNullOrEmpty(movie.TheatersIds))
                {
                    return new List<string>(); // Trả về danh sách trống nếu phim không tồn tại hoặc không có thông tin rạp
                }

                var theaterIds = movie.TheatersIds
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(id =>
                    {
                        return int.TryParse(id, out var parsedId) ? parsedId : (int?)null;
                    })
                    .Where(id => id.HasValue)
                    .Select(id => id.Value)
                    .ToList();

                if (!theaterIds.Any())
                {
                    return new List<string>(); // Trả về danh sách trống nếu không có ID rạp hợp lệ
                }

                var addresses = await _dbContext.Theaters
                    .Where(x => theaterIds.Contains(x.Id))
                    .Select(x => x.Address)
                    .Where(address => address != null) // Loại bỏ địa chỉ null
                    .Distinct() // Loại bỏ giá trị trùng lặp
                    .ToListAsync();

                return addresses;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving theater addresses.", ex);
            }
        }

        public async Task<List<TheaterViewModel>> GetTheaterDetails(int id, string address)
        {
            try
            {
                // Lấy thông tin phim theo Id
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == id)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        TheaterIds = x.TheatersIds
                    })
                    .FirstOrDefaultAsync();

                if (movie == null)
                {
                    throw new KeyNotFoundException("Movie not found with the specified Id.");
                }

                // In ra giá trị để kiểm tra
                Console.WriteLine("Movie Id: " + id);
                Console.WriteLine("Address: " + address);
                Console.WriteLine("TheaterIds: " + movie.TheaterIds);

                // Khai báo và chuyển TheaterIds thành danh sách các Id rạp
                var theaterIds = new List<int>();
                if (!string.IsNullOrEmpty(movie.TheaterIds))
                {
                    theaterIds = movie.TheaterIds
                        .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();
                }

                // In ra giá trị để kiểm tra
                Console.WriteLine("TheaterIds List: " + string.Join(", ", theaterIds));

                var theaterDetails = new List<TheaterViewModel>();

                // Nếu có các TheaterIds hợp lệ
                if (theaterIds.Any())
                {
                    // Chuyển địa chỉ thành chữ thường và loại bỏ khoảng trắng thừa
                    address = address.Trim().ToLower();

                    theaterDetails = await _dbContext.Theaters
                        .Where(t => theaterIds.Contains(t.Id) && t.Address.Trim().ToLower().Contains(address.ToLower()))
                        .Select(t => new TheaterViewModel
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Address = t.Address,
                            SubtitleTable_Id = t.SubtitleTable_Id,
                            Date = t.Date
                        })
                        .ToListAsync();
                }

                // In ra kết quả để kiểm tra
                Console.WriteLine("TheaterDetails Count: " + theaterDetails.Count);
                foreach (var theater in theaterDetails)
                {
                    Console.WriteLine($"Id: {theater.Id}, Name: {theater.Name}, Address: {theater.Address}");
                }

                return theaterDetails;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving theater details.", ex);
            }
        }


        public async Task<List<SubtitleTablesModel>> GetSubtitleTables(int movieId)
        {
            try
            {
                // Lấy thông tin movie dựa trên Id
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == movieId)
                    .Select(x => new
                    {
                        x.TheatersIds,
                    })
                    .FirstOrDefaultAsync();

                if (movie == null || string.IsNullOrEmpty(movie.TheatersIds))
                {
                    return new List<SubtitleTablesModel>(); // Trả về danh sách trống nếu phim không tồn tại hoặc không có thông tin rạp
                }

                // Lấy danh sách các ID rạp từ TheatersIds (client-side)
                var theaterIds = movie.TheatersIds
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value)
                    .ToList();

                if (!theaterIds.Any())
                {
                    return new List<SubtitleTablesModel>(); // Trả về danh sách trống nếu không có ID rạp hợp lệ
                }

                // Lấy danh sách Theaters dựa trên danh sách các ID rạp (client-side)
                var theaters = await _dbContext.Theaters
                    .Where(x => theaterIds.Contains(x.Id))
                    .ToListAsync();

                var subtitleTableIds = theaters
                    .SelectMany(x => x.SubtitleTable_Id
                        .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    .Distinct()
                    .ToList();

                if (!subtitleTableIds.Any())
                {
                    return new List<SubtitleTablesModel>(); // Trả về danh sách trống nếu không có SubtitleTable_Id hợp lệ
                }

                // Lấy danh sách SubtitleTables dựa trên danh sách subtitleTableIds
                var subtitleTables = await _dbContext.SubtitleTables
                    .Where(st => subtitleTableIds.Contains(st.Id.ToString()))
                    .Select(st => new SubtitleTablesModel
                    {
                        Id = st.Id,
                        Name = st.Name,
                        TimeSlot_Id = st.TimeSlot_Id
                    })
                    .ToListAsync();

                return subtitleTables;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving subtitle tables.", ex);
            }
        }

        public async Task<List<BookingModel>> GetBooking(int movie_Id, string theater_Address, int subtitleTable_Id)
        {
            try
            {
                // Bước 1: Lấy danh sách các ID rạp chiếu phim cho bộ phim
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == movie_Id)
                    .Select(x => x.TheatersIds)
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(movie))
                {
                    return new List<BookingModel>();
                }

                // Chuyển đổi chuỗi TheatersIds thành danh sách các số nguyên
                var theaterIds = movie
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value)
                    .ToList();

                if (!theaterIds.Any())
                {
                    return new List<BookingModel>();
                }

                // Bước 2: Lấy thông tin các rạp chiếu phim với địa chỉ cụ thể
                var theaters = await _dbContext.Theaters
                    .Where(x => theaterIds.Contains(x.Id) && x.Address == theater_Address)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name
                    })
                    .ToListAsync();

                if (!theaters.Any())
                {
                    return new List<BookingModel>();
                }

                // Bước 3: Lấy thông tin các subtitleTable và TimeSlotIds
                var subtitleTable = await _dbContext.SubtitleTables
                    .Where(x => x.Id == subtitleTable_Id)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        // Giả sử TimeSlotIds là một chuỗi phân cách bằng dấu phẩy
                        TimeSlotIds = x.TimeSlot_Id // Nếu TimeSlotIds đã là danh sách int, không cần chuyển đổi thêm
                    })
                    .FirstOrDefaultAsync();

                if (subtitleTable == null)
                {
                    return new List<BookingModel>();
                }

                // Chuyển đổi chuỗi TimeSlotIds thành danh sách các số nguyên
                var timeSlotIds = subtitleTable.TimeSlotIds
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value)
                    .ToList();

                if (!timeSlotIds.Any())
                {
                    return new List<BookingModel>();
                }

                // Bước 4: Lấy thông tin các TimeSlot
                var timeSlots = await _dbContext.TimeSlots
                    .Where(x => timeSlotIds.Contains(x.Id))
                    .Select(x => new
                    {
                        x.Id,
                        x.StartTime
                    })
                    .ToListAsync();

                // Bước 5: Kết hợp thông tin và tạo danh sách BookingModel
                var bookingModels = (from theater in theaters
                                     select new BookingModel
                                     {
                                         Theater_Id = theater.Id,
                                         Theater_Name = theater.Name,
                                         SubtitleTable_Id = subtitleTable.Id,
                                         SubtitleTable_Name = subtitleTable.Name,
                                         TimeSlots = timeSlots
                                             .Select(ts => new Shared.DTOs.Booking.TimeSlotModel
                                             {
                                                 Id = ts.Id,
                                                 StartTime = ts.StartTime
                                             })
                                             .ToList()
                                     }).ToList();

                return bookingModels;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving booking information.", ex);
            }
        }

        public async Task<string> LoadUserImage(int id)
        {
            try
            {
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (movie == null)
                {
                    throw new Exception($"Không tìm thấy bộ phim nào với ID {id}");
                }

                // Trả về đường dẫn hình ảnh của bộ phim
                return movie.Image;
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi lấy thông tin hình ảnh của bộ phim.", ex);
            }
        }

        public async Task<List<SubtitleTableTimeSlotsModel>> GetTimeSlot(int movieId, string nameSubtitleTable)
        {
            try
            {
                // Lấy thông tin movie dựa trên Id
                var movie = await _dbContext.Movies
                    .Where(x => x.Id == movieId)
                    .Select(x => new
                    {
                        x.TheatersIds,
                    })
                    .FirstOrDefaultAsync();

                if (movie == null || string.IsNullOrEmpty(movie.TheatersIds))
                {
                    return new List<SubtitleTableTimeSlotsModel>(); // Trả về danh sách trống nếu phim không tồn tại hoặc không có thông tin rạp
                }

                // Lấy danh sách các ID rạp từ TheatersIds
                var theaterIds = movie.TheatersIds
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value)
                    .ToList();

                if (!theaterIds.Any())
                {
                    return new List<SubtitleTableTimeSlotsModel>(); // Trả về danh sách trống nếu không có ID rạp hợp lệ
                }

                // Lấy danh sách Theaters dựa trên danh sách các ID rạp
                var theaters = await _dbContext.Theaters
                    .Where(x => theaterIds.Contains(x.Id))
                    .ToListAsync();

                var subtitleTableIds = theaters
                    .SelectMany(x => x.SubtitleTable_Id
                        .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    .Distinct()
                    .ToList();

                if (!subtitleTableIds.Any())
                {
                    return new List<SubtitleTableTimeSlotsModel>(); // Trả về danh sách trống nếu không có SubtitleTable_Id hợp lệ
                }

                // Lấy danh sách SubtitleTables dựa trên danh sách subtitleTableIds và điều kiện Name_SubtitleTable
                var subtitleTables = await _dbContext.SubtitleTables
                    .Where(st => subtitleTableIds.Contains(st.Id.ToString()) && st.Name == nameSubtitleTable)
                    .ToListAsync();

                if (!subtitleTables.Any())
                {
                    return new List<SubtitleTableTimeSlotsModel>(); // Trả về danh sách trống nếu không tìm thấy SubtitleTable phù hợp
                }

                // Tách TimeSlotIds từ SubtitleTables
                var subtitleTableTimeSlotIds = subtitleTables
                    .Select(st => new
                    {
                        st.Id,
                        st.Name,
                        TimeSlotIds = st.TimeSlot_Id
                            .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                            .Where(id => id.HasValue)
                            .Select(id => id.Value)
                            .ToList()
                    })
                    .ToList();

                var timeSlotIds = subtitleTableTimeSlotIds
                    .SelectMany(st => st.TimeSlotIds)
                    .Distinct()
                    .ToList();

                if (!timeSlotIds.Any())
                {
                    return new List<SubtitleTableTimeSlotsModel>(); // Trả về danh sách trống nếu không có TimeSlot_Id hợp lệ
                }

                // Lấy danh sách TimeSlots dựa trên danh sách timeSlotIds
                var timeSlots = await _dbContext.TimeSlots
                    .Where(ts => timeSlotIds.Contains(ts.Id))
                    .ToListAsync();

                // Nhóm TimeSlots theo Name_SubtitleTable
                var groupedTimeSlots = subtitleTableTimeSlotIds
                    .Select(st => new SubtitleTableTimeSlotsModel
                    {
                        Name_SubtitleTable = st.Name,
                        TimeSlots = timeSlots
                            .Where(ts => st.TimeSlotIds.Contains(ts.Id))
                            .Select(ts => new Shared.DTOs.SubtitleTableTimeSlots.TimeSlotModel
                            {
                                Id = ts.Id,
                                StartTime = ts.StartTime,
                                EndTime = ts.EndTime,
                                Date = ts.Date,
                            })
                            .ToList()
                    })
                    .ToList();

                return groupedTimeSlots;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving time slots.", ex);
            }
        }
    }
}
