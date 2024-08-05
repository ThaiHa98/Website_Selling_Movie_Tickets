using MediatR;
using Shared.DTOs.Ticket;
using Shared.SeedWork;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Create
{
    public class CreateTicketRequestHandler : IRequestHandler<CreateTicketRequest, Response<TicketModel>>
    {
        private readonly ITicketsRepository _ticketService;

        public CreateTicketRequestHandler(ITicketsRepository ticketService)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
        }

        public async Task<Response<TicketModel>> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _ticketService.AddAsync(request.TicketModel);
                if (!response.Success)
                {
                    throw new ApplicationException(response.Message);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating Tickets", ex);
            }
        }
    }
}
