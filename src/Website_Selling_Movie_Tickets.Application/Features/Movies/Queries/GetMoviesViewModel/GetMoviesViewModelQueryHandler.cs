using MediatR;
using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetMoviesViewModel
{
    //public class GetMoviesViewModelQueryHandler : IRequestHandler<GetMoviesViewModelQuery, MoviesViewModel>
    //{
    //    private readonly IMoviesRepository _moviesRepository;
    //    public GetMoviesViewModelQueryHandler(IMoviesRepository moviesRepository)
    //    {
    //        _moviesRepository = moviesRepository;
    //    }

    //    //public async Task<MoviesViewModel> Handle(GetMoviesViewModelQuery request, CancellationToken cancellationToken)
    //    //{
    //    //    //try
    //    //    //{
    //    //    //    var movie = await _moviesRepository.GetMoviesViewModel(request.movieId, request.premiere);
    //    //    //    if (movie == null)
    //    //    //    {
    //    //    //        throw new Exception("movieId && theaterId not found");
    //    //    //    }
    //    //    //    return movie;
    //    //    //}
    //    //    //catch (Exception ex) 
    //    //    //{
    //    //    //    throw new ApplicationException($"Error fetching movie with movieId: {request.movieId} and theaterId: {request.premiere}. Details: {ex.Message}");
    //    //    //}
    //    //}
    //}
}
