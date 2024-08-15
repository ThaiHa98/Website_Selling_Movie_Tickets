using MediatR;
using Shared.DTOs.Theater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetById
{
    public class GetByIdTheatersQuery : IRequest<TheaterModel>
    {
        public int Id { get; set; }
    }
}
