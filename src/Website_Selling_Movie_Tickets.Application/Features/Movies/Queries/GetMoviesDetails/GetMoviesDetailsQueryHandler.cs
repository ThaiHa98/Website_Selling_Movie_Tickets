using MediatR;
using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetMoviesDetails
{
    public class GetMoviesDetailsQueryHandler : IRequestHandler<GetMoviesDetailsQuery, MoviesViewModel>
    {
        private readonly IMoviesRepository _moviesRepository;
        public GetMoviesDetailsQueryHandler(IMoviesRepository moviesRepository) 
        {
            _moviesRepository = moviesRepository;
        }
        public async Task<MoviesViewModel> Handle(GetMoviesDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _moviesRepository.MoviesDetails(request.Id);
                if (result == null)
                {
                    throw new Exception("Data not found");
                }
                return result;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("Screening room not found", ex);
            }
        }
    }
}
