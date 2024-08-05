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
    public class CreateTicketRequest : IRequest<Response<TicketModel>>
    {
        public TicketModel TicketModel { get;}
        public CreateTicketRequest(TicketModel ticketModel)
        {
            TicketModel = ticketModel ?? throw new ArgumentNullException(nameof(ticketModel));
        }
    }
}
