using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetAll
{
    public class GetAllTheaterHandler : IRequestHandler<GetAllTheaterQuery, List<Theater>>
    {
        private readonly ITheaterRepository _theaterRepository;

        public GetAllTheaterHandler(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }

        public async Task<List<Theater>> Handle(GetAllTheaterQuery request, CancellationToken cancellationToken)
        {
            var query =  await _theaterRepository.GetAll();
            return query;
        }
    }
}
