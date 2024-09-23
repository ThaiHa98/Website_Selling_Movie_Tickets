using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.PopcornandDrinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Common.Update
{
    public class UpdatePopcornandDrinksRequestHandler : IRequestHandler<UpdatePopcornandDrinksRequest, PopcornandDrinksModel>
    {
        private readonly DBContext _dBContext;
        private readonly IPopcornandDrinksRepository _popcornandDrinksRepository;

        public UpdatePopcornandDrinksRequestHandler(DBContext dBContext, IPopcornandDrinksRepository popcornandDrinksRepository)
        {
            _dBContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
            _popcornandDrinksRepository = popcornandDrinksRepository ?? throw new ArgumentNullException(nameof(popcornandDrinksRepository));
            _dBContext.Database.EnsureCreated();
        }

        public async Task<PopcornandDrinksModel> Handle(UpdatePopcornandDrinksRequest request, CancellationToken cancellationToken)
        {
            var popcornandDrinks = await _dBContext.PopcornandDrinks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (popcornandDrinks == null)
            {
                throw new ArgumentException("PopcornandDrinks_Id not found");
            }

            popcornandDrinks.Price = request.Price;

            var updatedModel = new PopcornandDrinksModel
            {
                Id = popcornandDrinks.Id,
                Price = popcornandDrinks.Price
            };

            await _popcornandDrinksRepository.UpdateAsync(updatedModel);

            return updatedModel;
        }
    }
}
