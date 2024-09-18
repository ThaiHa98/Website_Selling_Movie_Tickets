using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class Ticket
    {
        public string Id { get; set; }
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public int Movies_Id { get; set; }
        public int TimeSlot_Id { get; set; }
        public int ChairType_Id { get;set; }
        public decimal ToatalPrice { get; set; }
        public int ScreeningRoom_Id {  get; set; }
        public int Theaters_Id { get; set; }
        public int Seat_Id { get; set; }
        public string SeatNumber { get; set; }
        public int SubtitleTable_Id { get; set; }
        public DateTime ShowTime { get; set; }
        public StatusTicket Status {  get; set; }
    }
}
