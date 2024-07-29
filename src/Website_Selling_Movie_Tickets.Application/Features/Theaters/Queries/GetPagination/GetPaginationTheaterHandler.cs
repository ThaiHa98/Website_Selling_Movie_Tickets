using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Application.Features.Genres.Queries.GetById;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetPagination
{
    public class GetPaginationTheaterHandler : IRequestHandler<GetPaginationTheaterQuery, Pagination<Theater>>
    {
        private readonly ITheaterRepository _theaterRepository;
        public GetPaginationTheaterHandler(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }
        public async Task<Pagination<Theater>> Handle(GetPaginationTheaterQuery request, CancellationToken cancellationToken)
        {
            return await _theaterRepository.GetPagination(request.PageIndex, request.PageSize);
        }
    }
}
