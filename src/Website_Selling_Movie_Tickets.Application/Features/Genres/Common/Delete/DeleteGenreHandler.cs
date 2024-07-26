using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Genres.Common.Delete
{
    public class DeleteGenreHandler : IRequestHandler<DeleteGenreRequest, string>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly DBContext _dbContext;
        public DeleteGenreHandler(IGenreRepository genreRepository, DBContext dbContext)
        {
            _genreRepository = genreRepository;
            _dbContext = dbContext;
        }

        public async Task<string> Handle(DeleteGenreRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.Genres.FirstOrDefault(x => x.Id == request.Id);
                if (query == null)
                {
                    throw new Exception("Id not found");
                }
                var deleteGenre = await _genreRepository.DeleteAsync(request.Id);
                return "Delete Successfully";
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while Delete the genre.", ex);
            }
        }
    }
}
