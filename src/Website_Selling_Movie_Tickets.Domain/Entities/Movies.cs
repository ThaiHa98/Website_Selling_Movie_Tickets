using System;
using System.Collections.Generic;
using System.Text.Json;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int GenreId { get; set; }
        public string RunningTime { get; set; } //bao nhiêu phút
        public DateTime Premiere { get; set; }//khởi chiếu
        public string Language { get; set; }//Phụ đề….
        public string Rated { get; set; }//xếp hạng
        public string Description { get; set; }
        public string Director { get; set; } //Đạo diễn
        public string Actors { get; set; } // danh sách diễn viên
        public string TheatersIds { get; set; }
        public int ScreeningRoom_Id { get; set; }
        public StatusMovie Status { get; set; }

        public List<string> GetActorList()
        {
            return JsonSerializer.Deserialize<List<string>>(Actors) ?? new List<string>();
        }

        public List<int> GetTheatersIds()
        {
            return JsonSerializer.Deserialize<List<int>>(TheatersIds) ?? new List<int>();
        }
    }
}