using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Delete
{
    public class DeleteTheatersHandler : IRequestHandler<DeleteTheatersRequest, string>
    {
        private readonly ITheaterRepository _heaterRepository;
        private DBContext _dbContext;
        public DeleteTheatersHandler(ITheaterRepository heaterRepository, DBContext dbContext)
        {
            _heaterRepository = heaterRepository;
            _dbContext = dbContext;
        }

        public async Task<string> Handle(DeleteTheatersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var theater = _dbContext.Theaters.FirstOrDefault(x => x.Id == request.Id);
                if (theater == null)
                {
                    throw new Exception("Id not found");
                }
                var deleteTheaters = await _heaterRepository.Delete(request.Id);
                return "Delete Successfully";
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while Delete the theater.", ex);
            }
        }
    }
}
