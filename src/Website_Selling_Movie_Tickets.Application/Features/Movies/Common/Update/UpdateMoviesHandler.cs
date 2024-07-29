using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Update
{
    public class UpdateMoviesHandler : IRequestHandler<UpdateMoviesRequest, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly DBContext _dbContext;

        public UpdateMoviesHandler(IMoviesRepository moviesRepository, DBContext dbContext)
        {
            _moviesRepository = moviesRepository;
            _dbContext = dbContext;
        }

        public async Task<Movie> Handle(UpdateMoviesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = await _dbContext.Movies
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (movie == null)
                {
                    throw new Exception("Movie Id not found");
                }
                movie.Premiere = request.Premiere.Value;
                await _dbContext.SaveChangesAsync(cancellationToken);
                return movie;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the movie.", ex);
            }
        }
    }
}
