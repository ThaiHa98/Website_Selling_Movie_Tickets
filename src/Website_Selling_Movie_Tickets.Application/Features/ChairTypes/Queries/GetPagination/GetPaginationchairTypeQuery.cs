using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Queries.GetPagination
{
    public class GetPaginationchairTypeQuery : IRequest<Response<Pagination<ChairType>>>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
