using MediatR;
using Shared.DTOs.SubtitleTable;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Queries.GetById
{
    public class GetByIdSubtitleTablesQuery : IRequest<Response<SubtitleTableModel>>
    {
        public int Id { get; set; }
    }
}
