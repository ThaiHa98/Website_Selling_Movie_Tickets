using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Create
{
    public class CreateMoviesRequest : IRequest<Movie>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public int GenreId { get; set; }
        public string RunningTime { get; set; }
        public DateTime Premiere { get; set; }
        public string Language { get; set; }
        public string Rated { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public List<string> Actor { get; set; }
        public List<int> TheatersIds { get; set; }
        public int ScreeningRoom_Id { get; set; }
        public StatusMovie Status { get; set; }

    }
}
