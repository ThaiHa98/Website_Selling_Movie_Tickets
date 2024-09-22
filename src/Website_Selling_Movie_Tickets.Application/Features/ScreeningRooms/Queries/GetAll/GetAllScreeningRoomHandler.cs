using MediatR;
using Shared.DTOs.ScreeningRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetAll
{
    public class GetAllScreeningRoomHandler : IRequestHandler<GetAllScreeningRoomQuery,ScreeningRoomModeSeatl>
    {
        private readonly IScreeningRoomRepository _screeningRoomRepository;
        public GetAllScreeningRoomHandler(IScreeningRoomRepository screeningRoomRepository)
        {
            _screeningRoomRepository = screeningRoomRepository ?? throw new ArgumentNullException(nameof(screeningRoomRepository));
        }
        public async Task<ScreeningRoomModeSeatl> Handle(GetAllScreeningRoomQuery request, CancellationToken cancellationToken)
        {
            var screeningRoom = await _screeningRoomRepository.GetAllScreeningMovieId(request.movie_Id);
            return screeningRoom;
        }
    }
}
