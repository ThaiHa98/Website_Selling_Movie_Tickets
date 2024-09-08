using MediatR;
using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetTheaterNames
{
    public class GetTheaterNamesQueryHandler : IRequestHandler<GetTheaterNamesQuery, List<TheaterViewModel>>
    {
        private readonly IMoviesRepository _moviesRepository;

        public GetTheaterNamesQueryHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<List<TheaterViewModel>> Handle(GetTheaterNamesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _moviesRepository.GetTheaterDetails(request.Id, request.address);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while processing GetTheaterName: {ex.Message}", ex);
            }
        }
    }
}
