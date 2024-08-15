using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Shared.DTOs.SubtitleTable;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Queries.GetAll
{
    public class GetAllSubtitleTablesQueryHandler : IRequestHandler<GetAllSubtitleTablesQuery, List<SubtitleTableModel>>
    {
        private readonly ISubtitleTableRepository _subtitleTableRepository;
        public GetAllSubtitleTablesQueryHandler(ISubtitleTableRepository subtitleTableRepository)
        {
            _subtitleTableRepository = subtitleTableRepository;
        }
        public async Task<List<SubtitleTableModel>> Handle(GetAllSubtitleTablesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subtitleTableRepository.GetAll(); // Ensure this method returns List<SubtitleTableModel>
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting all SubtitleTables!", ex);
            }
        }
    }
}
