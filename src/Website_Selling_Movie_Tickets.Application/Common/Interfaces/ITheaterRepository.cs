using Shared.DTOs.Theater;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface ITheaterRepository
    {
        Task<List<TheaterModel>> GetAll();
        Task<Pagination<Theater>> GetPagination(int pageIndex, int pageSize);
        Task<TheaterModel> GetById(int id);
        Task<Response<Theater>> Create(Theater entity);
        Task<Response<Theater>> Update(Theater entity);
        Task<Response<Theater>> Delete(int id);

    }
}
