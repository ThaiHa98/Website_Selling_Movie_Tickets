using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Genres.Queries.GetAll
{
    public class GetAllGenreQueryHandler : IRequestHandler<GetAllGenreQuery, List<Genre>>
    {
        private readonly IGenreRepository _genreRepository;
        public GetAllGenreQueryHandler(IGenreRepository genreRepository) 
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<Genre>> Handle(GetAllGenreQuery request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetAll();
            return genre;
        }
    }
}
