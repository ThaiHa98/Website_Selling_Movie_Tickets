using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Create
{
    public class CreateTheatersRequest : IRequest<Theater>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
    }
}
