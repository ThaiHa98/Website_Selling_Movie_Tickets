using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetImage
{
    public class GetImageMoviesQueryHandler : IRequestHandler<GetImageMoviesQuery, byte[]>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly DBContext _dbContext;

        public GetImageMoviesQueryHandler(IMoviesRepository moviesRepository, DBContext dbContext)
        {
            _moviesRepository = moviesRepository;
            _dbContext = dbContext;
        }

        public async Task<byte[]> Handle(GetImageMoviesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (movie == null)
                {
                    throw new Exception("Id not found");
                }

                // Lấy hình ảnh dưới dạng mảng byte từ repository
                var imageBytes = await _moviesRepository.GetMovieImageBytes(request.Id);
                return imageBytes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while searching for the movie.", ex);
            }
        }
    }
}
