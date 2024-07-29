using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Create
{
    public class CreateMoviesRequest : IRequest<Movie>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public int GenreId { get; set; }
        public string RunningTime { get; set; } //bao nhiêu phút
        public DateTime Premiere { get; set; }//khởi chiếu
        public string Language { get; set; }//Phụ đề….
        public string Rated { get; set; }//xếp hạng
        public string Description { get; set; }
        public string Director { get; set; } //Đạo diễn
        public List<string> Actor { get; set; }// diễn viên
    }
}
