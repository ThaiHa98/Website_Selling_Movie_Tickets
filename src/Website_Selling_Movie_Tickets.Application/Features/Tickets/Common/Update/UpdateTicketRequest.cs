using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Update
{
    public class UpdateTicketRequest : IRequest<Response<Ticket>>
    {
        public string Id { get; set; }
        public StatusTicket Status { get; set; }
    }
}
