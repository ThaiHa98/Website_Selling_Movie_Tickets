using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Queries.GetAll
{
    public class GetAllTicketQueryHandler : IRequestHandler<GetAllTicketQuery, List<Ticket>>
    {
        private readonly ITicketsRepository _ticketsRepository;
        public GetAllTicketQueryHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }
        public async Task<List<Ticket>> Handle(GetAllTicketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _ticketsRepository.GetAllTickets();
                return query;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while retrieving tickets", ex);
            }
        }
    }
}
