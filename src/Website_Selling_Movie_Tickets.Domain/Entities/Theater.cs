using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Domain.Entities
{
    public class Theater //Danh sách rạp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string SubtitleTable_Id { get; set; }
        public string ScreeningRoom_Id { get; set; }
        public string Date { get; set; }
    }
}
