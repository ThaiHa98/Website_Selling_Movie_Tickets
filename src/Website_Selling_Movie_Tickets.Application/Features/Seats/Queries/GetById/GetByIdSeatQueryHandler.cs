using MediatR;
using Shared.DTOs.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Seats.Queries.GetById
{
    public class GetByIdSeatQueryHandler : IRequestHandler<GetByIdSeatQuery, SeatModel>
    {
        private readonly ISeatRepository _seatRepository;
        public GetByIdSeatQueryHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }
        public async Task<SeatModel> Handle(GetByIdSeatQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var seat = await _seatRepository.GetSeatById(request.Id);
                if (seat == null)
                {
                    throw new Exception("Id not found");
                }
                return seat;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while retrieving Seat.", ex);
            }
        }
    }
}
