using MediatR;
using Shared.DTOs.Ticket;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Create
{
    public class CreateTicketRequest : IRequest<Response<Ticket>>
    {
        public int User_Id { get; set; }
        public int Movies_Id { get; set; }
        public int TimeSlot_Id { get; set; }
        public int ChairType_Id { get; set; }
        public int ScreeningRoom_Id { get; set; }
        public int Theaters_Id { get; set; }
        public List<string> Seat { get; set; }
        public int SubtitleTable_Id { get; set; }
        public DateTime ShowTime { get; set; }
        public StatusTicket Status { get; set; }
    }
}
