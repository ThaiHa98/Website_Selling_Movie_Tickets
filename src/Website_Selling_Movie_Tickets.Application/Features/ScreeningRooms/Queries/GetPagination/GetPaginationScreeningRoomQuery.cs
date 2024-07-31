﻿using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetPagination
{
    public class GetPaginationScreeningRoomQuery : IRequest<Pagination<ScreeningRoom>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
