using MediatR;
using Shared.DTOs.TimeSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetListTimeSlot
{
    public class GetListTimeSlotQuery : IRequest<List<ListTimeSlotModel>>
    {
        public int movie_Id { get; set; }
    }
}
