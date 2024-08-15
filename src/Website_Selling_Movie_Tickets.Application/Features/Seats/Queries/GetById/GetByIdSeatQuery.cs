using MediatR;
using Shared.DTOs.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Seats.Queries.GetById
{
    public class GetByIdSeatQuery : IRequest<SeatModel>
    {
        public int Id { get; set; }
    }
}
