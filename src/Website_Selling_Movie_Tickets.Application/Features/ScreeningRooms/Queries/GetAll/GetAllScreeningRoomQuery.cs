using MediatR;
using Shared.DTOs.ScreeningRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetAll
{
    public class GetAllScreeningRoomQuery : IRequest<ScreeningRoomModeSeatl>
    {
        public int movie_Id { get; set; }
    }
}
