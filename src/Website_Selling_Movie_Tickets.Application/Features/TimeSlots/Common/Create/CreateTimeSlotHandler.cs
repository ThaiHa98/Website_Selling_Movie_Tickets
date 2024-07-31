using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Common.Create
{
    public class CreateTimeSlotHandler : IRequestHandler<CreateTimeSlotRequest, TimeSlot>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        public CreateTimeSlotHandler(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository ?? throw new ArgumentNullException(nameof(timeSlotRepository));
        }
        public async Task<TimeSlot> Handle(CreateTimeSlotRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The request was not fully entered into the data fields");
                }
                var timeSlot = new TimeSlot
                {
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Date = request.Date,
                };
                var response = await _timeSlotRepository.AddAsync(timeSlot);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response.Data;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while creating the TimeSlot.", ex);
            }
        }
    }
}
