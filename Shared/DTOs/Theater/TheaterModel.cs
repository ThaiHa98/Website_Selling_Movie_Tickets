using Shared.DTOs.SubtitleTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Theater
{
    public class TheaterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<SubtitleTableModel> SubtitleTable { get; set; } = new List<SubtitleTableModel>();
        public string Date { get; set; }
    }

    public class SubtitleTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeSlot_Id { get; set; }
    }
}
