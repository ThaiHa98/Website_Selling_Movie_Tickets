using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Queries.GetPagination
{
    public class GetPaginationTimeSlotHandler : IRequestHandler<GetPaginationTimeSlotQuery, Pagination<TimeSlot>>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        public GetPaginationTimeSlotHandler(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }
        public async Task<Pagination<TimeSlot>> Handle(GetPaginationTimeSlotQuery request, CancellationToken cancellationToken)
        {
            return await _timeSlotRepository.GetPagination(request.PageIndex,request.PageSize);
             
        }
    }
}
