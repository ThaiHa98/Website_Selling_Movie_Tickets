using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Common.Delete
{
    public class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotRequest, bool>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly DBContext _dbContext;
        public DeleteTimeSlotHandler(ITimeSlotRepository timeSlotRepository, DBContext dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
            _timeSlotRepository = timeSlotRepository ?? throw new ArgumentNullException(nameof(timeSlotRepository));
        }

        public async Task<bool> Handle(DeleteTimeSlotRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var timeSlot = _dbContext.TimeSlots.FirstOrDefault(x => x.Id == request.Id);
                if (timeSlot == null)
                {
                    throw new Exception("Id not found");
                }
                var deleteTimeSlot = await _timeSlotRepository.RemoveAsync(request.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error update timeslot: {ex.Message}", ex);
            }
        }
    }
}
