using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Create
{
    public class CreateChairTypeRequest : IRequest<ChairType>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
