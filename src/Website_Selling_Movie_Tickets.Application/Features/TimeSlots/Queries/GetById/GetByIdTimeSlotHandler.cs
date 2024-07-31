using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Queries.GetById
{
    public class GetByIdTimeSlotHandler : IRequestHandler<GetByIdTimeSlotQuery, TimeSlot>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly DBContext _dbContext;

        public GetByIdTimeSlotHandler(ITimeSlotRepository timeSlotRepository, DBContext dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
            _timeSlotRepository = timeSlotRepository ?? throw new ArgumentNullException(nameof(timeSlotRepository));
        }

        public async Task<TimeSlot> Handle(GetByIdTimeSlotQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "Request cannot be null");
                }

                var timeSlot = await _timeSlotRepository.GetById(request.Id);
                if (timeSlot == null)
                {
                    throw new Exception("TimeSlot not found");
                }

                return timeSlot;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the timeslot.", ex);
            }
        }
    }
}
