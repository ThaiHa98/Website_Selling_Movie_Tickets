using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Common.Create
{
    public class CreateTimeSlotRequest : IRequest<TimeSlot>
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}
