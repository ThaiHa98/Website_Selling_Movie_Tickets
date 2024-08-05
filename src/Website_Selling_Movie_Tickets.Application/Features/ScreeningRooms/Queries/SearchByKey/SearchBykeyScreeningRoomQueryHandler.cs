using MediatR;
using Shared.SeedWork;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.SearchByKey
{
    public class SearchBykeyScreeningRoomQueryHandler : IRequestHandler<SearchByKeyScreeningRoomQuery, Pagination<ScreeningRoom>>
    {
        private readonly IScreeningRoomRepository _screeningRoomRepository;

        public SearchBykeyScreeningRoomQueryHandler(IScreeningRoomRepository screeningRoomRepository)
        {
            _screeningRoomRepository = screeningRoomRepository;
        }

        public async Task<Pagination<ScreeningRoom>> Handle(SearchByKeyScreeningRoomQuery request, CancellationToken cancellationToken)
        {
            return await _screeningRoomRepository.SearchByKeyAsync(request.key, request.pageIndex, request.pageSize);
        }
    }
}
