using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.SubtitleTable
{
    public class SubtitleTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TimeSlotDetails> TimeSlots { get; set; } = new List<TimeSlotDetails>();
    }

    public class TimeSlotDetails
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}
