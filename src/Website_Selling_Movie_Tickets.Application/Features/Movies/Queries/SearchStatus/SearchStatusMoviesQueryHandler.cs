using MediatR;
using Shared.DTOs.MoviesView;
using Shared.DTOs.SearchStatusMovies;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchStatus;
using Website_Selling_Movie_Tickets.Domain.Entities;

public class SearchStatusMoviesQueryHandler : IRequestHandler<SearchStatusMoviesQuery, List<MoviesViewModel>>
{
    private readonly IMoviesRepository _moviesRepository;

    public SearchStatusMoviesQueryHandler(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }

    public async Task<List<MoviesViewModel>> Handle(SearchStatusMoviesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _moviesRepository.SearchStatusAsync(request.status);
            if (result == null)
            {
                throw new ArgumentException("Data not found");
            }
            return result;
        }
        catch (Exception ex) 
        {
            throw new ApplicationException("An error occurred while processing the request.", ex);
        }
    }
}

