using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.TimeSlot
{
    public class ListTimeSlotModel
    {
        public int Id { get; set; }
        public string Name_SubtitleTable { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}
