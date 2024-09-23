using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Queries.GetImage
{
    public class GetImagePopcornandDrinksQuery : IRequest<byte[]>
    {
        public int Id { get; set; }
    }
}
