using MediatR;
using Shared.DTOs.PopcornandDrinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Common.Update
{
    public class UpdatePopcornandDrinksRequest : IRequest<PopcornandDrinksModel>
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}
