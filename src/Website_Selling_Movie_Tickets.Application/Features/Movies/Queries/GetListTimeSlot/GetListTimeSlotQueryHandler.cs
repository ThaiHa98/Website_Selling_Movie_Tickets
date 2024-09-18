using MediatR;
using Shared.DTOs.SubtitleTableTimeSlots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetListTimeSlot
{
    public class GetListTimeSlotQueryHandler : IRequestHandler<GetListTimeSlotQuery, List<SubtitleTableTimeSlotsModel>>
    {
        private readonly IMoviesRepository _moviesRepository;

        public GetListTimeSlotQueryHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }
        public async Task<List<SubtitleTableTimeSlotsModel>> Handle(GetListTimeSlotQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _moviesRepository.GetTimeSlot(request.movie_Id,request.nameSubtitleTable);
                if (result == null)
                {
                    throw new Exception("Data not found");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while processing GetSubtitleTable: {ex.Message}", ex);
            }
        }
    }
}
