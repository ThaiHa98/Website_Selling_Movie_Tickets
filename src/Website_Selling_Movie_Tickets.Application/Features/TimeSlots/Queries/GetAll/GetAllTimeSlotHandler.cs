using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Application.Common.Repositories;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Queries.GetAll
{
    public class GetAllTimeSlotHandler : IRequestHandler<GetAllTimeSlotQuery, List<TimeSlot>>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        public GetAllTimeSlotHandler(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<List<TimeSlot>> Handle(GetAllTimeSlotQuery request, CancellationToken cancellationToken)
        {
            var query = await _timeSlotRepository.GetAll();
            return query;
        }
    }
}
