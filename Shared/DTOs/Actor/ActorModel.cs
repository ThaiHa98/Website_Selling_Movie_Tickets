using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Actor
{
    public class ActorModel
    {
        [Key]
        public int Id { get; set; } // Khóa chính
        public string Name { get; set; }
    }
}
