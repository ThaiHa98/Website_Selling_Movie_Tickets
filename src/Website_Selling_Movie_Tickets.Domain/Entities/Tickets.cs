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
        public int Movies_Id { get; set; }
        public int TimeSlot_Id { get; set; }
        public int ChairType_Id { get;set; }
        public decimal ChairType_Price { get; set; }
        public int ScreeningRoom_Id {  get; set; }
        public int Theaters_Id { get; set; }
        public StatusTicket Status {  get; set; }
    }
}
