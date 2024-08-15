using MediatR;
using Shared.DTOs.MoviesView;
using Shared.DTOs.SearchStatusMovies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchStatus
{
    public class SearchStatusMoviesQuery : IRequest<List<MoviesViewModel>>
    {
        public string status {  get; set; }
    }
}
