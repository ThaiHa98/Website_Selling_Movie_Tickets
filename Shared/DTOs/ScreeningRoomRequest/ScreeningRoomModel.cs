using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Actor
{
    public class ScreeningRoomModel
    {
        public string Name { get; set; }
        public int Numberofseats { get; set; }
        public int NumberOfRegularSeat { get; set; }
        public int NumberOfVIPSeat { get; set; }
        public int NumberOfLoveBoxes { get; set; }
    }
}
