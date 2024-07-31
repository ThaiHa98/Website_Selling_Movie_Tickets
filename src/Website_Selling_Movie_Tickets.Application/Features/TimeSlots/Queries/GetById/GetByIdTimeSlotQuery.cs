using MediatR;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Queries.GetById
{
    public class GetByIdTimeSlotQuery : IRequest<TimeSlot>
    {
        public int Id { get; set; }
    }
}
