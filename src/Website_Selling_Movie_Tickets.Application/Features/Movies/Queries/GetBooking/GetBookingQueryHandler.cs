using MediatR;
using Shared.DTOs.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetBooking
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, List<BookingModel>>
    {
        private readonly IMoviesRepository _moviesRepository;
        public GetBookingQueryHandler(IMoviesRepository moviesRepository) 
        {
            _moviesRepository = moviesRepository;
        }
        public async Task<List<BookingModel>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _moviesRepository.GetBooking(request.movie_Id, request.theater_Address, request.subtitleTable_Id);
                if (result == null || !result.Any())
                {
                    throw new Exception("Data not found");
                }
                return result;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while GetBooking.", ex);
            }
        }
    }
}
