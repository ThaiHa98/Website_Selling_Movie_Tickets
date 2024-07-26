using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Genres.Queries.GetById
{
    public class GetByIdGenreHandler : IRequestHandler<GetByIdGenreQuery, Genre>
    {
        private readonly IGenreRepository _genreRepository;
        public GetByIdGenreHandler(IGenreRepository genreRepository) 
        {
            _genreRepository = genreRepository;
        }
        public async Task<Genre> Handle(GetByIdGenreQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The request was not fully entered into the data fields");
                }
                var genre = await _genreRepository.GetById(request.Id);
                if (genre == null)
                {
                    throw new ArgumentException(nameof(genre), "Data not found");
                }
                return genre;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while retrieving GetById.", ex);
            }
        }
    }
}
