using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchByKey
{
    public class SearchByKeyMovieQueryHandler : IRequestHandler<SearchByKeyMovieQuery, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;

        public SearchByKeyMovieQueryHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<Movie> Handle(SearchByKeyMovieQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.key))
            {
                return null;
            }

            try
            {
                var movie = await _moviesRepository.SearchByKeyAsync(request.key);
                return movie;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while searching for the movie.", ex);
            }
        }
    }
}
