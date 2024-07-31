using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetById
{
    public class GetByIdScreeningRoomQuery : IRequest<ScreeningRoom>
    {
        public int Id { get; set; }
    }
}
