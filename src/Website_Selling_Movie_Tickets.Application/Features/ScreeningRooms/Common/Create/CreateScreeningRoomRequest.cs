using MediatR;
using Shared.DTOs.Actor;
using Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Create
{
    public class CreateScreeningRoomRequest : IRequest<ScreeningRoomModel>
    {
        public ScreeningRoomModel ScreeningRoomModel {  get; }
        public CreateScreeningRoomRequest(ScreeningRoomModel screeningRoomModel) 
        {
            ScreeningRoomModel = screeningRoomModel ?? throw new ArgumentNullException(nameof(screeningRoomModel));
        }
    }
}
