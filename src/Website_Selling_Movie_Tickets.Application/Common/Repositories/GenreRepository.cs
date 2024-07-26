using Microsoft.EntityFrameworkCore;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DBContext _dbContext;
        public GenreRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Genre> AddAsync(Genre genre)
        {
            _dbContext.Genres.Add(genre);
            await _dbContext.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> DeleteAsync(int id)
        {
            var genre = await _dbContext.Genres.FindAsync(id);
            if (genre == null)
            {
                return null;
            }
            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync();
            return genre;
        }


        public async Task<Pagination<Genre>> GetAll(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.Genres.CountAsync();
            var items = await _dbContext.Genres
                                        .Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
            return new Pagination<Genre>(pageIndex, pageSize,totalRecords,items);
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        public async Task<Genre> GetById(int id)
        {
            var genres = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
            return genres;
        }

        public async Task<Genre> UpdateAsync(Genre genre)
        {
            _dbContext.Update(genre);
            await _dbContext.SaveChangesAsync();
            return genre;
        }
    }
}
