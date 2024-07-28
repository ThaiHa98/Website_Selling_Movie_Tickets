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
    public class MoviesRepository : IMoviesRepository
    {
        private readonly DBContext _dbContext;
        public MoviesRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public Movies Create(Movies movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public bool Delete(Movies movie)
        {
            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Movies> GetAll()
        {
            return _dbContext.Movies.ToList();
        }

        public Movies GetById(int id)
        {
            var movies = _dbContext.Movies.FirstOrDefault(x => x.Id == id);
            if (movies == null) 
            {
                throw new Exception("Id not found");
            }
            return movies;
        }

        public Pagination<Movies> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = _dbContext.Movies.Count();
            var item = _dbContext.Movies
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();
            return new Pagination<Movies>(pageIndex, pageSize, totalRecords,item);
        }

        public Movies Update(Movies movie)
        {
            _dbContext.Update(movie);
            _dbContext.SaveChanges();
            return movie;
        }
    }
}
