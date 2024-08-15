using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.SearchStatusMovies
{
    public class MoviesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string RunningTime { get; set; }
        public DateTime Premiere { get; set; }
        public string Language { get; set; }
        public string Rated { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Status { get; set; }
    }
}
