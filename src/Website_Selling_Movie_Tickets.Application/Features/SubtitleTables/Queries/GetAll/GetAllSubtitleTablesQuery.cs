using MediatR;
using Shared.DTOs.SubtitleTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Queries.GetAll
{
    public class GetAllSubtitleTablesQuery : IRequest<List<SubtitleTableModel>>
    {
        public GetAllSubtitleTablesQuery() { }
    }
}
