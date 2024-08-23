using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Shared.DTOs.MoviesView; // Thêm thư viện này nếu cần

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchIronfilmreleased
{
    public class SearchIronfilmreleasedMovieQueryHandler : IRequestHandler<SearchIronfilmreleasedMovieQuery, List<MoviesViewModel>>
    {
        private readonly IMoviesRepository _moviesRepository;

        public SearchIronfilmreleasedMovieQueryHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<List<MoviesViewModel>> Handle(SearchIronfilmreleasedMovieQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.key))
            {
                return null;
            }

            try
            {
                var movies = await _moviesRepository.SearchIronfilmreleased(request.key);
                return movies;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while searching for the movie.", ex);
            }
        }
    }
}
