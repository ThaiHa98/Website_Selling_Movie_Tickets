using MediatR;
using Shared.DTOs.PopcornandDrinks;
using System.Collections.Generic;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Queries.GetAll
{
    public class GetAllPopcornandDrinksQuery : IRequest<List<PopcornandDrink>>
    {
        public GetAllPopcornandDrinksQuery() { }
    }
}
