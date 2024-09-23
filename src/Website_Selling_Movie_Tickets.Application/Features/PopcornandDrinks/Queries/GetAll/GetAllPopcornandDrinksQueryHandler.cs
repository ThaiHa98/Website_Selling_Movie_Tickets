using MediatR;
using Shared.DTOs.PopcornandDrinks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Queries.GetAll
{
    public class GetAllPopcornandDrinksQueryHandler : IRequestHandler<GetAllPopcornandDrinksQuery, List<PopcornandDrink>>
    {
        private readonly IPopcornandDrinksRepository _repository;
        private readonly DBContext _dBContext;

        public GetAllPopcornandDrinksQueryHandler(IPopcornandDrinksRepository popcornandDrinksRepository, DBContext dbContext)
        {
            _dBContext = dbContext;
            _repository = popcornandDrinksRepository;
        }
        public async Task<List<PopcornandDrink>> Handle(GetAllPopcornandDrinksQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetAllAsync();
            return response.Data;
        }
    }
}
