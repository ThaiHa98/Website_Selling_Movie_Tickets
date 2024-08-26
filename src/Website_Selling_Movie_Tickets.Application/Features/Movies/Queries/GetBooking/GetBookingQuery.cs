using MediatR;
using Shared.DTOs.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetBooking
{
    public class GetBookingQuery : IRequest<List<BookingModel>>
    {
        public int movie_Id { get; set; }
        public string theater_Address { get; set; }
        public int subtitleTable_Id { get; set; }
    }
}
