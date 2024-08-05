using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Update
{
    public class UpdateChairTypeHandler : IRequestHandler<UpdateChairTypeRequest, ChairType>
    {
        private readonly IChairTypeRepository _chairTypeRepository;
        private readonly DBContext _dbContext;
        public UpdateChairTypeHandler(IChairTypeRepository chairTypeRepository, DBContext dbContext)
        {
            _chairTypeRepository = chairTypeRepository;
            _dbContext = dbContext;
        }

        public async Task<ChairType> Handle(UpdateChairTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var chairType = _dbContext.ChairTypes.FirstOrDefault(x => x.Id == request.Id);
                if (chairType == null)
                {
                    throw new Exception("Id not found");
                }
                chairType.Price = request.Price;
                var response = await _chairTypeRepository.UpdateAsync(chairType);
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
                throw new ApplicationException("An error occurred while updating the theater.");
            }
        }
    }
}
