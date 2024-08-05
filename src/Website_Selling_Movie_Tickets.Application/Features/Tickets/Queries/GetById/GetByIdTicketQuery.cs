using MediatR;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Queries.GetById
{
    public class GetByIdTicketQuery : IRequest<Ticket>
    {
        public string Id { get; set; }
    }
}
