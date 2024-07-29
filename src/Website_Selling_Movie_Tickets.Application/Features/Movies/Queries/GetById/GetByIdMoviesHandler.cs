using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetById
{
    public class GetByIdMoviesHandler : IRequestHandler<GetByIdMoviesQuery, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly DBContext _dbContext;
        public GetByIdMoviesHandler(IMoviesRepository moviesRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _moviesRepository = moviesRepository;
        }
        public async Task<Movie> Handle(GetByIdMoviesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == request.Id);
                if (movie == null)
                {
                    throw new Exception("Id not found");
                }
                var getById = await _moviesRepository.GetById(request.Id);
                return movie;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while GetById the movies.",ex);
            }
        }
    }
}
