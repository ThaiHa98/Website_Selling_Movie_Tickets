using Shared.DTOs.PopcornandDrinks;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface IPopcornandDrinksRepository
    {
        Task<Response<PopcornandDrink>> AddAsync (PopcornandDrink popcornandDrinks);
        Task<string> UpdateAsync (PopcornandDrinksModel popcornandDrinksModel);
        Task<bool> DeleteAsync (int id);
        Task<Response<List<PopcornandDrink>>> GetAllAsync();
        Task<Response<PopcornandDrink>> GetById(int id);
        Task<Response<Pagination<PopcornandDrink>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<byte[]> GetPopcornandDrinkImageBytes(int id);
    }
}
