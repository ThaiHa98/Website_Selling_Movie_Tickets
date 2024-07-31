using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Common.Update
{
    public class UpdateTimeSlotHandler : IRequestHandler<UpdateTimeSlotRequest, string>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly DBContext _dbContext;

        public UpdateTimeSlotHandler(ITimeSlotRepository timeSlotRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<string> Handle(UpdateTimeSlotRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var timeSlot = _dbContext.TimeSlots.FirstOrDefault(x => x.Id == request.Id);
                if (timeSlot == null)
                {
                    throw new Exception("Id not found");
                }

                timeSlot.StartTime = request.StartTime;
                timeSlot.EndTime = request.EndTime;
                timeSlot.Date = request.Date;

                var response = await _timeSlotRepository.UpdateAsync(timeSlot);

                if (response.Success)
                {
                    return response.Message;
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating timeslot: {ex.Message}", ex);
            }
        }
    }
}
