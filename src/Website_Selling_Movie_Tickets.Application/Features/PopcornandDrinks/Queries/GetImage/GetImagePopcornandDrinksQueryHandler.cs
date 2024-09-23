using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Queries.GetImage
{
    public class GetImagePopcornandDrinksQueryHandler : IRequestHandler<GetImagePopcornandDrinksQuery, byte[]>
    {
        private readonly DBContext _dBContext;
        private readonly IPopcornandDrinksRepository _popcornandDrinksRepository;

        public GetImagePopcornandDrinksQueryHandler(DBContext dbContext, IPopcornandDrinksRepository popcornandDrinksRepository)
        {
            _dBContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _popcornandDrinksRepository = popcornandDrinksRepository ?? throw new ArgumentNullException(nameof(popcornandDrinksRepository));
        }
        public async Task<byte[]> Handle(GetImagePopcornandDrinksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = await _dBContext.PopcornandDrinks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (movie == null)
                {
                    throw new Exception("Id not found");
                }
                var imageBytes = await _popcornandDrinksRepository.GetPopcornandDrinkImageBytes(request.Id);
                return imageBytes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while searching for the movie.", ex);
            }
        }
    }
}
