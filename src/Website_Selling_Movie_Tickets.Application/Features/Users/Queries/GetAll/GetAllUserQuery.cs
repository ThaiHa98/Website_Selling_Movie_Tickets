using MediatR;
using Shared.SeedWork;
using System.Collections.Generic;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Queries.GetAll
{
    public class GetAllUserQuery : IRequest<Pagination<User>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
