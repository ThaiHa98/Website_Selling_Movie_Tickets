using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Ticket
{
    public class TicketModel
    {
        public int User_Id { get; set; }
        public int Movies_Id { get; set; }
        public int TimeSlot_Id { get; set; }
        public List<int> ChairType_Id { get; set; }
        public int ScreeningRoom_Id { get; set; }
        public int Theaters_Id { get; set; }
        public int Seat_Id { get; set; }
        public string SeatNumber { get; set; }

    }
}
