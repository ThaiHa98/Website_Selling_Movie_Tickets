using MediatR;
using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetTheaterNames
{
    public class GetTheaterNamesQuery : IRequest<List<TheaterViewModel>>
    {
        public int Id { get; set; }
        public string address { get; set; }
    }
}
