using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Showtimes_Id { get; set; } //lịch chiếu
        public int GenreId { get; set; }
        public DateTime RunningTime { get; set; } //bao nhiêu phút
        public DateTime Premiere {  get; set; }//khởi chiếu
        public string Language { get; set; }//Phụ đề….
        public string Rated {  get; set; }//xếp hạng
        public string Description { get; set; }
    }
}
