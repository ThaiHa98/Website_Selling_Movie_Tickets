using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Genres.Common.Create
{
    public class CreateGenreHandler : IRequestHandler<CreateGenreRequest, Genre>
    {
        private readonly IGenreRepository _genreRepository;
        public CreateGenreHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<Genre> Handle(CreateGenreRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The request was not fully entered into the data fields");
                }
                var genre = new Genre
                {
                    Name = request.Name
                };
                var createdGenre = await _genreRepository.AddAsync(genre);
                return genre;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while creating the genre.", ex);
            }
        }
    }
}
