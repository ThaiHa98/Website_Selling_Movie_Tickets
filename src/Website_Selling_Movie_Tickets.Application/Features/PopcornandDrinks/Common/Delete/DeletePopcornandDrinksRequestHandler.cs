using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Common.Delete
{
    public class DeletePopcornandDrinksRequestHandler : IRequestHandler<DeletePopcornandDrinksRequest, PopcornandDrink>
    {
        private readonly DBContext _dbContext;
        private readonly IPopcornandDrinksRepository _popcornandDrinksRepository;

        public DeletePopcornandDrinksRequestHandler(DBContext dbContext, IPopcornandDrinksRepository popcornandDrinksRepository)
        {
            _dbContext = dbContext;
            _popcornandDrinksRepository = popcornandDrinksRepository;
        }

        public async Task<PopcornandDrink> Handle(DeletePopcornandDrinksRequest request, CancellationToken cancellationToken)
        {
            var popcornandDrinks = await _dbContext.PopcornandDrinks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (popcornandDrinks == null)
            {
                throw new Exception("PopcornandDrinks_Id not found");
            }
            await _popcornandDrinksRepository.DeleteAsync(request.Id);
            return popcornandDrinks;
        }
    }
}
