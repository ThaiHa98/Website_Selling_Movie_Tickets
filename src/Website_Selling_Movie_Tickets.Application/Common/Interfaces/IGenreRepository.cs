using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface IGenreRepository 
    {
        Task<List<Genre>> GetAll();
        Task<Pagination<Genre>> GetAll(int page, int pageSize);
        Task<Genre> GetById(int id);
        Task<Genre> AddAsync(Genre genre);
        Task<Genre> UpdateAsync(Genre genre);
        Task<Genre> DeleteAsync(int id);
    }
}
