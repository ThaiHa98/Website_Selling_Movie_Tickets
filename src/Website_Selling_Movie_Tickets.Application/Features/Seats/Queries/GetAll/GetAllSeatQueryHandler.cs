using MediatR;
using Shared.DTOs.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Seats.Queries.GetAll
{
    public class GetAllSeatQueryHandler : IRequestHandler<GetAllSeatQuery,List<SeatModel>>
    {
        private readonly ISeatRepository _seatRepository;
        public GetAllSeatQueryHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<List<SeatModel>> Handle(GetAllSeatQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _seatRepository.GetAll();
                return result;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while getting all Seat!", ex);
            }
        }
    }
}
