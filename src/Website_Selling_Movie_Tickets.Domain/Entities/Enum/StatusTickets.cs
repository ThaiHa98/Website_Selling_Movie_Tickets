using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Domain.Entities.Enum
{
    public enum StatusTicket
    {
        Open,        // Vé mới được mở
        InProgress,  // Vé đang được xử lý
        Resolved,    // Vé đã được giải quyết
        Closed,      // Vé đã được đóng
        Reopened     // Vé đã được mở lại
    }
}
