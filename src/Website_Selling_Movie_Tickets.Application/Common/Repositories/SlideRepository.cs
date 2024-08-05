using Microsoft.EntityFrameworkCore;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class SlideRepository : ISlideRepository
    {
        private readonly DBContext _dbContext;
        public SlideRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Response<Slide>> Create(Slide slide)
        {
            var request = new Slide
            {
                Name = slide.Name,
                Description = slide.Description,
                Url = slide.Url,
                Image = slide.Image,
                Sort = slide.Sort,
                Status = StatusSlide.Active,
            };
            _dbContext.Slides.Add(slide);
            var result = await _dbContext.SaveChangesAsync();
            var query = new Slide
            {
                Name = slide.Name,
                Description = slide.Description,
                Url = slide.Url,
                Image = slide.Image,
                Sort = slide.Sort,
            };
            if (result > 0) 
            {
                return new Response<Slide>
                {
                    Data = query,
                    Success = true,
                    Message = "Slide has been added successfully."
                };
            }
            else
            {
                return new Response<Slide>
                {
                    Success = false,
                    Message = "An error occurred while adding the Ticket."
                };
            }
        }

        public async Task<Response<Slide>> Delete(int id)
        {
            var slide = _dbContext.Slides.SingleOrDefault(s => s.Id == id);
            _dbContext.Slides.Remove(slide);
            var reusult = await _dbContext.SaveChangesAsync();
            if (reusult > 0)
            {
                return new Response<Slide>
                {
                    Data = slide,
                    Success = true,
                    Message = "Slide has been added successfully"
                };
            }
            else
            {
                return new Response<Slide>
                {
                    Success = true,
                    Message = "Failed to add Slide"
                };
            }
        }

        public async Task<List<Slide>> GetAll()
        {
            return await _dbContext.Slides.ToListAsync();
        }

        public async Task<Slide> GetById(int id)
        {
            var slide = await _dbContext.Slides.FirstOrDefaultAsync(x => x.Id == id);
            return slide;
        }

        public async Task<Pagination<Slide>> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.Slides.CountAsync();
            var items = _dbContext.Slides
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new Pagination<Slide>(pageIndex, pageSize, totalRecords, items);
        }

        public async Task<Response<Slide>> Update(Slide slide)
        {
            _dbContext.Slides.Update(slide);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<Slide>
                {
                    Success = true,
                    Data = slide,
                    Message = "Slides has been Update successfully."
                };
            }
            else
            {
                return new Response<Slide>
                {
                    Success = true,
                    Message = "Failed to update Slide."
                };
            }
        }
    }
}
