using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.SearchByKey
{
    public class SearchByKeyScreeningRoomQuery : IRequest<Pagination<ScreeningRoom>>
    {
        public string key { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
