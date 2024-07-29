using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetPagination
{
    public class GetPaginationMoviesHandler : IRequestHandler<GetPaginationMoviesQuery, Pagination<Movie>>
    {
        private readonly IMoviesRepository _moviesRepository;
        public GetPaginationMoviesHandler(IMoviesRepository moviesRepository) 
        {
            _moviesRepository = moviesRepository;
        }
        public async Task<Pagination<Movie>> Handle(GetPaginationMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _moviesRepository.GetPagination(request.pageIndex, request.pageSize);

        }
    }
}
