using MediatR;
using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchIronfilmreleased
{
    public class SearchIronfilmreleasedMovieQuery : IRequest<List<MoviesViewModel>>
    {
        public string key { get; set; }
    }
}
