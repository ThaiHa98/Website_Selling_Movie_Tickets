using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Domain.Entities.Enum
{
    public enum StatusMovie
    {
        New,           // Phim mới
        ComingSoon,    // Phim sắp chiếu
        NowShowing,    // Phim đang chiếu
        Finished,      // Phim đã chiếu xong
        Canceled       // Phim bị hủy
    }
}
