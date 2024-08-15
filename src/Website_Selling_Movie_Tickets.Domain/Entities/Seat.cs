using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int ScreeningRoom_Id { get; set; }
        public int ChairType_Id { get; set; }
        public string Row {  get; set; }
        public string Number { get; set; }
        public StatusSeat Status { get; set; }
    }
}
