using MediatR;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Update
{
    public class UpdateTheaterRequest : IRequest<Theater>
    {
        public int Id { get; set; }
        public string Address { get; set; }
    }
}
