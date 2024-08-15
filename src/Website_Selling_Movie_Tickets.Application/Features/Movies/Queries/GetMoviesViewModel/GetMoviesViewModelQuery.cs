using MediatR;
using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetMoviesViewModel
{
    public class GetMoviesViewModelQuery : IRequest<MoviesViewModel>
    {
        public int movieId { get; set; }
    }
}
