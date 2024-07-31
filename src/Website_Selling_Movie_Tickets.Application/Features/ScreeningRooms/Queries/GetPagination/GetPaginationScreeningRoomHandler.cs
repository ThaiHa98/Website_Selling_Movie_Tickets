using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetPagination
{
    public class GetPaginationScreeningRoomHandler : IRequestHandler<GetPaginationScreeningRoomQuery, Pagination<ScreeningRoom>>
    {
        private readonly IScreeningRoomRepository _screeningRoomRepository;
        public GetPaginationScreeningRoomHandler(IScreeningRoomRepository screeningRoomRepository)
        {
            _screeningRoomRepository = screeningRoomRepository;
        }

        public async Task<Pagination<ScreeningRoom>> Handle(GetPaginationScreeningRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("request not found");
                }
                var getPagination = await _screeningRoomRepository.GetPagination(request.PageIndex, request.PageSize);
                return getPagination;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while getPagination the screeningroom");
            }
        }
    }
}
