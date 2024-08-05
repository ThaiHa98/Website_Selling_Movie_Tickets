using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Domain.Entities.Enum
{
    public enum StatusNameChairType
    {
        [Description("Regular Seat")]
        RegularSeat,
        [Description("VIP Seat")]
        VIPSeat,
        [Description("Love Boxes")]
        LoveBoxes
    }
}
