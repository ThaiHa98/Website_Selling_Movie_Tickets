using AutoMapper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Create
{
    public class CreateSlidesRequestHandler : IRequestHandler<CreateSlidesRequest, Response<Slide>>
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IConfiguration _configuration;
        public CreateSlidesRequestHandler(ISlideRepository slideRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _slideRepository = slideRepository;
        }

        public async Task<Response<Slide>> Handle(CreateSlidesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The request was not fully entered into the data fields");
                }

                var imagePath = await SaveImageAsync(request.Image);

                var query = new Slide
                {
                    Name = request.Name,
                    Description = request.Description,
                    Url = request.Url,
                    Image = imagePath,
                    Sort = request.Sort,
                    Status = request.Status,
                };
                var response = await _slideRepository.Create(query);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response;
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while creating the Slide.", ex);
            }
        }
        private async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                string currentDataFolder = DateTime.Now.ToString("dd-MM-yyyy");
                var baseFolder = _configuration.GetValue<string>("BaseAddress");

                // Tạo thư mục Slide
                var slideFolder = Path.Combine(baseFolder, "Slide");

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
                return Path.Combine("Slide", currentDataFolder, fileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the image: {ex.Message}");
            }
        }
    }
}