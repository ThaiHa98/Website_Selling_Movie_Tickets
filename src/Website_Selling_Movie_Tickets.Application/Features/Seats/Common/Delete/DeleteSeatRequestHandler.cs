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

namespace Website_Selling_Movie_Tickets.Application.Features.Seats.Common.Delete
{
    public class DeleteSeatRequestHandler : IRequestHandler<DeleteSeatRequest, Seat>
    {
        private readonly ISeatRepository _seatRepository;
        private readonly DBContext _dbContext;
        public DeleteSeatRequestHandler(ISeatRepository seatRepository, DBContext dbContext)
        {
            _seatRepository = seatRepository;
            _dbContext = dbContext;
        }
        public async Task<Seat> Handle(DeleteSeatRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var seat = await _dbContext.Seats.FirstOrDefaultAsync(x => x.Id == request.SeatId);
                if (seat == null) 
                {
                    throw new Exception("Id not found");
                }
                var response = await _seatRepository.DeleteAsync(request.SeatId);
                return seat;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while Delete the Seat.", ex);
            }
        }
    }
}
