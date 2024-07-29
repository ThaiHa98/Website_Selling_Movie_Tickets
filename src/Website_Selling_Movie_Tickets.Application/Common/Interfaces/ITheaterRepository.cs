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
        Task<List<Theater>> GetAll();
        Task<Pagination<Theater>> GetPagination(int pageIndex, int pageSize);
        Task<Theater> GetById(int id);
        Theater Create(Theater entity);
        Task<Theater> Update(Theater entity);
        Task<Theater> Delete(int id);

    }
}
