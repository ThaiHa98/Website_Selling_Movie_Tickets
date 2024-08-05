using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Delete
{
    public class DeleteTicketRequestHandler : IRequestHandler<DeleteTicketRequest, Response<Ticket>>
    {
        private readonly ITicketsRepository _ticketRepository;
        private readonly DBContext _dbContext;
        public DeleteTicketRequestHandler(ITicketsRepository ticketRepository, DBContext dbContext)
        {
            _ticketRepository = ticketRepository;
            _dbContext = dbContext;
        }
        public async Task<Response<Ticket>> Handle(DeleteTicketRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = _dbContext.Tickets.FirstOrDefault(x => x.Id == request.Id);
                if (ticket == null)
                {
                    throw new Exception("Id not found");
                }
                var response = await _ticketRepository.DeleteAsync(request.Id);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while delete Ticket.", ex);
            }
        }
    }
}
