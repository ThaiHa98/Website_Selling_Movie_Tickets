using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Genres.Common.Update
{
    public class UpdateGenreHandler : IRequestHandler<UpdateGenreRequest, string>
    {
        private readonly IGenreRepository _genreRepository;
        private DBContext _dbContext;
        public UpdateGenreHandler(IGenreRepository genreRepository, DBContext dbContext)
        {
            _genreRepository = genreRepository;
            _dbContext = dbContext;
        }

        public async Task<string> Handle(UpdateGenreRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == request.Id);
                if (genre == null)
                {
                    throw new Exception("Id not found");
                }
                genre.Name = request.Name;
                await _genreRepository.UpdateAsync(genre);
                return "Update Successfully";
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while updating the genre.", ex);
            }
        }
    }
}
