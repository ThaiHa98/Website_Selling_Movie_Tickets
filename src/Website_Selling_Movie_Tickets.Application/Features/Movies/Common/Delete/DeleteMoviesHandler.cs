using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Delete
{
    public class DeleteMoviesHandler : IRequestHandler<DeleteMoviesRequest, bool>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly DBContext _dbContext;
        public DeleteMoviesHandler(IMoviesRepository moviesRepository, DBContext dbContext) 
        {
            _moviesRepository = moviesRepository;
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(DeleteMoviesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == request.Id);
                if (movie == null)
                {
                    throw new Exception("Id not found");
                }
                var deleteMovie = await _moviesRepository.Delete(movie);
                return true;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while delete the genre.", ex);
            }
        }
    }
}
