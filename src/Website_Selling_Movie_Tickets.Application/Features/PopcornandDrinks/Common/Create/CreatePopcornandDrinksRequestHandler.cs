using AutoMapper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.SeedWork;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Common.Create
{
    public class CreatePopcornandDrinksRequestHandler : IRequestHandler<CreatePopcornandDrinksRequest, Response<PopcornandDrink>>
    {
        private readonly IPopcornandDrinksRepository _ipopcornandDrinksRepository;
        private readonly IConfiguration _configuration;

        public CreatePopcornandDrinksRequestHandler(IPopcornandDrinksRepository ipopcornandDrinksRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _ipopcornandDrinksRepository = ipopcornandDrinksRepository;
        }

        public async Task<Response<PopcornandDrink>> Handle(CreatePopcornandDrinksRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Data not entered enough data.");
            }

            var imagePath = await SaveImageAsync(request.Image);
            var popcornandDrink = new PopcornandDrink
            {
                Name = request.Name,
                Image = imagePath,
                Description = request.Description,
                Quantity = request.Quantity,
                Price = request.Price,
            };

            var result = await _ipopcornandDrinksRepository.AddAsync(popcornandDrink);
            if (result == null)
            {
                throw new ApplicationException("Error occurred while adding the popcorn and drink.");
            }

            return new Response<PopcornandDrink>
            {
                Success = true,
                Data = result.Data,
                Message = "Popcorn and drink added successfully."
            };
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                string currentDataFolder = DateTime.Now.ToString("dd-MM-yyyy");
                var baseFolder = _configuration.GetValue<string>("BaseAddress");

                var PopcornandDrinks = Path.Combine(baseFolder, "PopcornandDrink");

                if (!Directory.Exists(PopcornandDrinks))
                {
                    Directory.CreateDirectory(PopcornandDrinks);
                }

                var folderPath = Path.Combine(PopcornandDrinks, currentDataFolder);

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

                return Path.Combine("PopcornandDrink", currentDataFolder, fileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the image: {ex.Message}");
            }
        }
    }
}
