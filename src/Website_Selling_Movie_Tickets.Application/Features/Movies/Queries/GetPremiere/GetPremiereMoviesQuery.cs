using MediatR;
using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetById
{
    public class GetPremiereMoviesQuery : IRequest<List<TheaterViewModel>>
    {
        public int Id { get; set; }
        public DateTime premiere { get; set; }
    }
}
