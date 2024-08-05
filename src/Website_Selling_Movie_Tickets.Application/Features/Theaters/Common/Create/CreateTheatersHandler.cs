using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Create
{
    public class CreateTheatersHandler : IRequestHandler<CreateTheatersRequest, Theater>
    {
        private readonly ITheaterRepository _theaterRepository;

        public CreateTheatersHandler(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository ?? throw new ArgumentNullException(nameof(theaterRepository));
        }

        public async Task<Theater> Handle(CreateTheatersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The request was not fully entered into the data fields");
                }

                var theater = new Theater
                {
                    Name = request.Name,
                    Address = request.Address,
                    Date = request.Date,

                };

                var response = await _theaterRepository.Create(theater);

                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response.Data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the theater.", ex);
            }
        }
    }
}
