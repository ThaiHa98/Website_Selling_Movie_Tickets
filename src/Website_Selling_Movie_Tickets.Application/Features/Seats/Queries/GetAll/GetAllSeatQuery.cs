using MediatR;
using Shared.DTOs.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Seats.Queries.GetAll
{
    public class GetAllSeatQuery : IRequest<List<SeatModel>>
    {
        public GetAllSeatQuery() { }
    }
}
