using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetAll
{
    public class GetAllMoviesHandler : IRequestHandler<GetAllMoviesQuery,List<Movie>>
    {
        private readonly IMoviesRepository _moviesRepository;
        public GetAllMoviesHandler(IMoviesRepository moviesRepository) 
        {
            _moviesRepository = moviesRepository;
        }
        public async Task<List<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var getAll = await _moviesRepository.GetAll();
            return getAll;
        }
    }
}
