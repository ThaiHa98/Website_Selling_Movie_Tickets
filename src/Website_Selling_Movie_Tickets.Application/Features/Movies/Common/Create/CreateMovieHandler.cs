using AutoMapper.Configuration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;


namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Create
{
    public class CreateMovieHandler : IRequestHandler<CreateMoviesRequest, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly DBContext _dbContext;
        private readonly IConfiguration _configuration;
        private const string USER_CONTENT_FOLDER_NAME = "user-forder";
        public CreateMovieHandler(IMoviesRepository moviesRepository, DBContext dbContext, IConfiguration configuration)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Movie> Handle(CreateMoviesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == request.GenreId);
                if (genre == null)
                {
                    throw new ArgumentException("Genre Id not found", nameof(request.GenreId));
                }

                var TheatersId = new List<int>();
                foreach (var theaterId in request.TheatersIds)
                {
                    var theater = await _dbContext.Theaters.FirstOrDefaultAsync(x => x.Id == theaterId);
                    if (theater == null)
                    {
                        throw new Exception($"Theater with Id {theaterId} not found");
                    }
                    TheatersId.Add(theater.Id);
                }

                var imagePath = await SaveImageAsync(request.Image);
                var movie = new Movie
                {
                    Name = request.Name,
                    Image = imagePath,
                    GenreId = genre.Id,
                    RunningTime = request.RunningTime,
                    Premiere = request.Premiere,
                    Language = request.Language,
                    Rated = request.Rated,
                    Description = request.Description,
                    Director = request.Director,
                    Actors = string.Join(", ", request.Actor),
                    TheatersIds = string.Join(", ", TheatersId),
                    Status = request.Status,
                };

                var response = await _moviesRepository.Create(movie);
                if (!response.Success)
                {
                    throw new ApplicationException(response.Message);
                }

                return response.Data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the movie.", ex);
            }
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                string currentDataFolder = DateTime.Now.ToString("dd-MM-yyyy");
                var baseFolder = _configuration.GetValue<string>("BaseAddress");

                // Tạo thư mục Slide
                var slideFolder = Path.Combine(baseFolder, "Movie");

                if (!Directory.Exists(slideFolder))
                {
                    Directory.CreateDirectory(slideFolder);
                }

                // Tạo thư mục date nằm trong thư mục Slide
                var folderPath = Path.Combine(slideFolder, currentDataFolder);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Trả về tên thư mục và tên ảnh
                return Path.Combine("Movie", currentDataFolder, fileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the image: {ex.Message}");
            }
        }

        //private async Task<string> SaveFile(IFormFile file)
        //{
        //    try
        //    {
        //        // Lấy tên tệp gốc từ ContentDisposition và tạo tên tệp mới với Guid để đảm bảo tính duy nhất
        //        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

        //        // Lấy BaseAddress từ configuration
        //        var baseFolder = _configuration.GetValue<string>("BaseAddress");

        //        // Tạo thư mục dữ liệu hiện tại theo ngày
        //        string currentDataFolder = DateTime.Now.ToString("dd-MM-yyyy");
        //        var movieFolder = Path.Combine(baseFolder, "Movie");
        //        var dateFolder = Path.Combine(movieFolder, currentDataFolder);

        //        // Tạo thư mục nếu chưa tồn tại
        //        if (!Directory.Exists(dateFolder))
        //        {
        //            Directory.CreateDirectory(dateFolder);
        //        }

        //        // Kết hợp đường dẫn với tên file để tạo đường dẫn đầy đủ
        //        var filePath = Path.Combine(dateFolder, fileName);

        //        // Mở file stream để ghi dữ liệu vào đường dẫn đã xác định
        //        using var output = new FileStream(filePath, FileMode.Create);

        //        // Sao chép nội dung stream của file vào file đích
        //        await file.OpenReadStream().CopyToAsync(output);

        //        // Trả về đường dẫn đầy đủ của file lưu trữ
        //        return $"/Movie/{currentDataFolder}/{fileName}";
        //    }
        //    catch (Exception ex)
        //    {
        //        // Bắt và ném lại bất kỳ ngoại lệ nào xảy ra
        //        throw new ApplicationException("An error occurred while saving the file.", ex);
        //    }
        //}
    }
}
