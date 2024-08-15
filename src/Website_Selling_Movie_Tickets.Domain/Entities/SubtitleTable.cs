using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class SubtitleTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeSlot_Id { get; set; }
    }
}
