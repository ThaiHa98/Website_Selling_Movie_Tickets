using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Application.Features.Seats.Common.Update
{
    public class UpdateSeatRequest : IRequest<Seat>
    {
        public int Id { get; set; }
        public StatusSeat Status { get; set; }
    }
}
