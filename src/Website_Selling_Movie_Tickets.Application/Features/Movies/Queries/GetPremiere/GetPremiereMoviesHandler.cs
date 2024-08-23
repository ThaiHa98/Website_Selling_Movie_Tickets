using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.MoviesView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetById
{
    public class GetPremiereMoviesHandler : IRequestHandler<GetPremiereMoviesQuery, List<TheaterViewModel>>
    {
        private readonly IMoviesRepository _moviesRepository;

        public GetPremiereMoviesHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<List<TheaterViewModel>> Handle(GetPremiereMoviesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _moviesRepository.GetPremiere(request.Id, request.premiere);
                if (result == null || !result.Any())
                {
                    throw new Exception("No theaters found for the specified movie and premiere date.");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving theaters.", ex);
            }
        }
    }
}
