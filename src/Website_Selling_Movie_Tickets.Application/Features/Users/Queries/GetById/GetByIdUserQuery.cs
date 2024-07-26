using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}
