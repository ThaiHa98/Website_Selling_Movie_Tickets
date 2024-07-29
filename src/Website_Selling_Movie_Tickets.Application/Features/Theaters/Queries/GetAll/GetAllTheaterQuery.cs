using MediatR;
using System.Collections.Generic;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetAll
{
    public class GetAllTheaterQuery : IRequest<List<Theater>>
    {
        public GetAllTheaterQuery() { }
    }
}
