using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Domain.Entities.Enum
{
    public enum StatusScreenigRoom
    {
        Available,   // Phòng chiếu sẵn sàng sử dụng
        Maintenance, // Phòng chiếu đang bảo trì
        Closed       // Phòng chiếu bị đóng
    }
}
