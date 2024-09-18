using MediatR;
using Shared.DTOs.SubtitleTableTimeSlots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetListTimeSlot
{
    public class GetListTimeSlotQuery : IRequest<List<SubtitleTableTimeSlotsModel>>
    {
        public int movie_Id { get; set; }
        public string nameSubtitleTable { get; set; }
    }
}
