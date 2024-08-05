using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Delete
{
    public class DeleteChairTypeHandler : IRequestHandler<DeleteChairTypeRequest, ChairType>
    {
        private readonly IChairTypeRepository _chairTypeRepository;
        private readonly DBContext _dbContext;
        public DeleteChairTypeHandler(IChairTypeRepository chairTypeRepository, DBContext dbContext)
        {
            _chairTypeRepository = chairTypeRepository;
            _dbContext = dbContext;
        }

        public async Task<ChairType> Handle(DeleteChairTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var chairType = await _dbContext.ChairTypes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (chairType == null)
                {
                    throw new Exception("ChairType not found");
                }

                var response = await _chairTypeRepository.DeleteAsync(request.Id);
                if (response.Success)
                {
                    return response.Data;
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while deleting ChairType: {ex.Message}");
            }
        }
    }
}
