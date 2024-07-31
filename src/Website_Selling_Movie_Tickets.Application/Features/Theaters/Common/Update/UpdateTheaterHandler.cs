using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Update
{
    public class UpdateTheaterHandler : IRequestHandler<UpdateTheaterRequest, Theater>
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly DBContext _dbContext;

        public UpdateTheaterHandler(ITheaterRepository theaterRepository, DBContext dbContext)
        {
            _dbContext = dbContext;
            _theaterRepository = theaterRepository;
        }

        public async Task<Theater> Handle(UpdateTheaterRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "The request was not fully entered into the data fields");
            }

            var query = await _dbContext.Theaters.FindAsync(request.Id);
            if (query == null)
            {
                throw new Exception("Id not found");
            }

            query.Date = request.Date;

            try
            {
                var response = await _theaterRepository.Update(query);
                if (response.Success)
                {
                    return response.Data;
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the theater.", ex);
            }
        }
    }
}
