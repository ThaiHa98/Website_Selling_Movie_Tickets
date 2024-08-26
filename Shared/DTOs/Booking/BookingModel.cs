using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Booking
{
    public class BookingModel
    {
        public int Theater_Id { get; set; }
        public string Theater_Name { get; set; }
        public int SubtitleTable_Id { get; set; }
        public string SubtitleTable_Name { get; set; }
        public List<TimeSlotModel> TimeSlots { get; set; } = new List<TimeSlotModel>();
    }
    public class TheaterModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; } // Thêm thông tin tên rạp
    }

    public class SubtitleTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // Thêm thông tin tên subtitleTable
    }

    public class TimeSlotModel
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
    }
}
