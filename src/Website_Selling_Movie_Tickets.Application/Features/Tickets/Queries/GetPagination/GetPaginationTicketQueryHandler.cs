using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Queries.GetPagination
{
    public class GetPaginationTicketQueryHandler : IRequestHandler<GetPaginationTicketQuery, Pagination<Ticket>>
    {
        private readonly ITicketsRepository _ticketRepository;
        public GetPaginationTicketQueryHandler(ITicketsRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Pagination<Ticket>> Handle(GetPaginationTicketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ticketRepository.GetAllTicketsPagination(request.pageIndex, request.pageSize);
                if (request == null) 
                {
                    throw new ArgumentNullException(nameof(request), "request not found");
                }
                return result;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while getPagination the Ticket");
            }
        }
    }
}
