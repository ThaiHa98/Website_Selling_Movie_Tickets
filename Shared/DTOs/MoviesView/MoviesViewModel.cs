using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.MoviesView
{
    public class MoviesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int GenreId { get; set; }
        public string Genre_Name { get; set; }
        public string RunningTime { get; set; }
        public DateTime Premiere { get; set; }
        public string Language { get; set; }
        public string Rated { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public List<TheaterViewModel> Theaters { get; set; } = new List<TheaterViewModel>(); // Đảm bảo thuộc tính này là đúng
    }

    public class TheaterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string SubtitleTable_Id { get; set; }
        public string Date { get; set; }
        public List<SubtitleTableModel> SubtitleTable { get; set; } // Đổi thành List
    }

    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubtitleTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeSlot_Id { get; set; }
    }

}
