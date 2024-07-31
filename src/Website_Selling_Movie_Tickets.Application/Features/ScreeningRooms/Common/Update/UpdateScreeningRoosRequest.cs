using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Update
{
    public class UpdateScreeningRoosRequest : IRequest<string>
    {
        public int Id { get; set; }
        public StatusScreenigRoom Status { get; set; }
    }
}
