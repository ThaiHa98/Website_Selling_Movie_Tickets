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
    public class GetByIdMoviesHandler : IRequestHandler<GetByIdMoviesQuery, MoviesViewModel>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly DBContext _dbContext;

        public GetByIdMoviesHandler(IMoviesRepository moviesRepository, DBContext dbContext)
        {
            _dbContext = dbContext;
            _moviesRepository = moviesRepository;
        }

        public async Task<MoviesViewModel> Handle(GetByIdMoviesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _moviesRepository.GetById(request.Id, request.premiere);
                if (result == null)
                {
                    throw new Exception("Data not found");
                }
                return result;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("Screening room not found", ex);
            }
        }
    }
}
