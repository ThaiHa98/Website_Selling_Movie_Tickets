using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface IMoviesRepository
    {
        List<Movies> GetAll();
        Pagination<Movies> GetPagination(int pageIndex, int pageSize);
        Movies GetById(int id);
        public Movies Create(Movies movie);
        Movies Update(Movies movie);
        bool Delete(Movies movie);
    }
}
