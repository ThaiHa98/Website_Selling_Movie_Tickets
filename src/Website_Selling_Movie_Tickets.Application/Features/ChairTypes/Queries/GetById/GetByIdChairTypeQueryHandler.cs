using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Queries.GetById
{
    public class GetByIdChairTypeQueryHandler : IRequestHandler<GetByIdChairTypeQuery,ChairType>
    {
        private readonly IChairTypeRepository _chairTypeRepository;
        private readonly DBContext _dbContext;
        public GetByIdChairTypeQueryHandler(IChairTypeRepository chairTypeRepository, DBContext dbContext)
        {
            _chairTypeRepository = chairTypeRepository;
            _dbContext = dbContext;
        }

        public async Task<ChairType> Handle(GetByIdChairTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var chairType = _dbContext.ChairTypes.FirstOrDefault(x => x.Id == request.Id);
                if (chairType == null)
                {
                    throw new Exception("Id not found");
                }
                var response = await _chairTypeRepository.GetById(request.Id);
                return response;
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while retrieving GetById.", ex);
            }
        }
    }
}
