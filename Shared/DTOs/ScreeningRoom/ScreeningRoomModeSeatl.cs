using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.ScreeningRoom
{
    public class ScreeningRoomModeSeatl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Numberofseats { get; set; }
        public int NumberOfRegularSeat { get; set; }
        public int NumberOfVIPSeat { get; set; }
        public int NumberOfLoveBoxes { get; set; }
        public List<Seat> Seats { get; set; }
    }

    public class Seat
    {
        public int Id { get; set; }
        public int ScreeningRoom_Id { get; set; }
        public int ChairType_Id { get; set; }
        public string Row { get; set; }
        public string Number { get; set; }
    }
}
