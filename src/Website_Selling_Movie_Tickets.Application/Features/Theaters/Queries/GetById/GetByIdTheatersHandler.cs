using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetById
{
    public class GetByIdTheatersHandler : IRequestHandler<GetByIdTheatersQuery, Theater>
    {
        private readonly ITheaterRepository _theaterRepository;
        public GetByIdTheatersHandler(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }

        public async Task<Theater> Handle(GetByIdTheatersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "Request cannot be null");
                }

                var theater = await _theaterRepository.GetById(request.Id);

                if (theater == null)
                {
                    throw new Exception("Theater not found");
                }

                return theater;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while GetById the movies.", ex);
            }
        }
    }
}
