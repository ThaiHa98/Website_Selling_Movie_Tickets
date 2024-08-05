using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Queries.GetById
{
    public class GetByIdTicketQueryHandler : IRequestHandler<GetByIdTicketQuery, Ticket>
    {
        private readonly ITicketsRepository _ticketRepository;

        public GetByIdTicketQueryHandler(ITicketsRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> Handle(GetByIdTicketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketsById(request.Id);
                if (ticket == null)
                {
                    throw new Exception("Ticket not found");
                }
                return ticket;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving ticket by Id", ex);
            }
        }
    }
}
