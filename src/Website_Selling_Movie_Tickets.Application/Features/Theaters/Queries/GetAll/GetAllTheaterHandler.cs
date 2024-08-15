using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetAll;
using Shared.DTOs.Theater;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetAll
{
    public class GetAllTheaterHandler : IRequestHandler<GetAllTheaterQuery, List<TheaterModel>>
    {
        private readonly ITheaterRepository _theaterRepository;

        public GetAllTheaterHandler(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }

        public async Task<List<TheaterModel>> Handle(GetAllTheaterQuery request, CancellationToken cancellationToken)
        {
            var theaters = await _theaterRepository.GetAll();
            return theaters;
        }
    }
}
