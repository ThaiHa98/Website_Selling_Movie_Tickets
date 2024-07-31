using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface IChairTypeRepository 
    {
        Task<Response<ChairType>> AddAsync(ChairType chairType);
        Task<Response<ChairType>> UpdateAsync(ChairType chairType);
        Task<Response<ChairType>> DeleteAsync(int id);
        Task<Response<List<ChairType>>> GetAll();
        Task<Response<Pagination<ChairType>>> GetPagination(int pageIndex, int pageSize);

    }
}
