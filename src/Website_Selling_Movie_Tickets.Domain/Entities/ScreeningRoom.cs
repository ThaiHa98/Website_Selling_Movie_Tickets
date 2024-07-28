using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class ScreeningRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Numberofseats { get; set; }
        public StatusScreenigRoom Status {  get; set; }

    }
}
