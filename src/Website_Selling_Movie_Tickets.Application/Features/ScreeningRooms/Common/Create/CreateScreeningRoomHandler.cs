using MediatR;
using Shared.DTOs.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Create
{
    public class CreateScreeningRoomHandler : IRequestHandler<CreateScreeningRoomRequest, ScreeningRoomModel>
    {
        private readonly IScreeningRoomRepository _screeningRoomRepository;
        public CreateScreeningRoomHandler(IScreeningRoomRepository screeningRoomRepository)
        {
            _screeningRoomRepository = screeningRoomRepository;
        }

        public async Task<ScreeningRoomModel> Handle(CreateScreeningRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.ScreeningRoomModel == null)
                {
                    throw new Exception("Requested data not fully entered");
                }

                // Thêm ScreeningRoom và nhận phản hồi
                var response = await _screeningRoomRepository.AddAsync(request.ScreeningRoomModel);

                if (!response.Success)
                {
                    throw new ApplicationException(response.Message);
                }

                return response.Data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the room reference", ex);
            }
        }

    }
}
