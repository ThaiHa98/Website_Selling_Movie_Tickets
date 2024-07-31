using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Update
{
    public class UpdateScreeningRoomHandler : IRequestHandler<UpdateScreeningRoosRequest, string>
    {
        private readonly IScreeningRoomRepository _screeningRoomRepository;
        private readonly DBContext _dbContext;
        public UpdateScreeningRoomHandler(IScreeningRoomRepository screeningRoomRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _screeningRoomRepository = screeningRoomRepository;
        }
        public async Task<string> Handle(UpdateScreeningRoosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var screeningRoom = _dbContext.ScreeningRooms.FirstOrDefault(x => x.Id == request.Id);
                if (screeningRoom == null)
                {
                    throw new Exception("Id not found");
                }
                screeningRoom.Status = request.Status;
                var update = await _screeningRoomRepository.UpdateAsync(screeningRoom);
                return update;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while Update the screeningRoom", ex);
            }
        }
    }
}
