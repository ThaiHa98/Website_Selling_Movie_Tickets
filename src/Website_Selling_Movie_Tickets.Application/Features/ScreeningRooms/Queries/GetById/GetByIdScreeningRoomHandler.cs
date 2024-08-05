using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetById
{
    public class GetByIdScreeningRoomHandler : IRequestHandler<GetByIdScreeningRoomQuery, ScreeningRoom>
    {
        private readonly IScreeningRoomRepository _screeningRoomRepository;
        public GetByIdScreeningRoomHandler(IScreeningRoomRepository screeningRoomRepository)
        {
            _screeningRoomRepository = screeningRoomRepository;
        }

        public async Task<ScreeningRoom> Handle(GetByIdScreeningRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("request not found");
                }
                var response = await _screeningRoomRepository.GetById(request.Id);
                if (response.Success) 
                {
                    return response.Data;
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("Screening room not found", ex);
            }
        }
    }
}
