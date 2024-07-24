using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class Tickets
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public int Showtime_Id { get; set; }
        public string Seat { get;set; }
        public int ChairType_Id { get; set;}
        public decimal ChairType_Price { get;set; }
        public StatusTicket ChairType_Volume { get; set; }
    }
}
