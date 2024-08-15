using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Common.Delete
{
    public class DeleteSubtitleTableRequestHandler : IRequestHandler<DeleteSubtitleTableRequest, SubtitleTable>
    {
        private readonly ISubtitleTableRepository _subtitleTableRepository;
        private readonly DBContext _dbContext;
        public DeleteSubtitleTableRequestHandler(ISubtitleTableRepository subtitleTableRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _subtitleTableRepository = subtitleTableRepository;
        }

        public async Task<SubtitleTable> Handle(DeleteSubtitleTableRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var subtitleTable = _dbContext.SubtitleTables.FirstOrDefault(x => x.Id == request.Id);
                if (subtitleTable == null)
                {
                    throw new Exception("Id not found");
                }
                var result = await _subtitleTableRepository.DeleteAsync(request.Id);
                return subtitleTable;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while Delete the SubtitleTable.", ex);
            }
        }
    }
}
