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

namespace Website_Selling_Movie_Tickets.Application.Features.Seats.Common.Update
{
    public class UpdateSeatRequestHandler : IRequestHandler<UpdateSeatRequest, Seat>
    {
        private readonly ISeatRepository _repository;
        private readonly DBContext _dbContext;
        public UpdateSeatRequestHandler(ISeatRepository repository, DBContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<Seat> Handle(UpdateSeatRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var seat = await _dbContext.Seats.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (seat == null)
                {
                    throw new Exception("Id not found");
                }
                seat.Status = request.Status;
                return seat;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while updating the Seat.",ex);
            }
        }
    }
}
