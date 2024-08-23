using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetTheaterAddressesByMovieId
{
    public class GetTheaterAddressesByMovieIdQueryHandler : IRequestHandler<GetTheaterAddressesByMovieIdQuery, List<string>>
    {
        private readonly IMoviesRepository _moviesRepository;
        public GetTheaterAddressesByMovieIdQueryHandler(IMoviesRepository moviesRepository) 
        {
            _moviesRepository = moviesRepository;
        }
        public async Task<List<string>> Handle(GetTheaterAddressesByMovieIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var theaterAddresses = await _moviesRepository.GetTheaterAddressesByMovieId(request.Id);
                return theaterAddresses;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException($"An error occurred while processing GetTheaterName: {ex.Message}",ex);
            }
        }
    }
}
