using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class ShowTimes
    {
        public int Id { get; set; }
        public int Movies_Id { get; set; }
        public int Theaters_Id { get; set; }
        public string Theaters_Name { get;set; }
        public string Theaters_Address { get; set; }
        public int TimeSlot_Id { get; set; }
    }
}
