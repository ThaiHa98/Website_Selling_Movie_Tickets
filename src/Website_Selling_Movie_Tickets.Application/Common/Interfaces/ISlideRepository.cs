using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface ISlideRepository
    {
        Task<List<Slide>> GetAll();
        Task<Pagination<Slide>> GetPagination(int pageIndex, int pageSize);
        Task<Slide> GetById(int id);
        Task<Response<Slide>> Create(Slide slide);
        Task<Response<Slide>> Update(Slide slide);
        Task<Response<Slide>> Delete(int id);
    }
}
