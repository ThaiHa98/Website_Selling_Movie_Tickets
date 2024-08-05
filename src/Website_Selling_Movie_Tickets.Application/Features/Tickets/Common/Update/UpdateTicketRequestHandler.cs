using MediatR;
using Shared.SeedWork;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Update
{
    public class UpdateTicketRequestHandler : IRequestHandler<UpdateTicketRequest, Response<Ticket>>
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly DBContext _dbContext;

        public UpdateTicketRequestHandler(ITicketsRepository ticketsRepository, DBContext dbContext)
        {
            _ticketsRepository = ticketsRepository ?? throw new ArgumentNullException(nameof(ticketsRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Response<Ticket>> Handle(UpdateTicketRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (ticket == null)
                {
                    return new Response<Ticket>
                    {
                        Success = false,
                        Message = "Ticket Id not found"
                    };
                }

                ticket.Status = request.Status;
                var response = await _ticketsRepository.UpdateAsync(ticket);

                if (response.Success)
                {
                    return new Response<Ticket>
                    {
                        Data = response.Data,
                        Success = true,
                        Message = "Ticket has been updated successfully"
                    };
                }
                else
                {
                    return new Response<Ticket>
                    {
                        Success = false,
                        Message = response.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<Ticket>
                {
                    Success = false,
                    Message = $"An error occurred while updating the ticket: {ex.Message}"
                };
            }
        }
    }
}
