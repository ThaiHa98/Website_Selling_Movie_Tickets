using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Delete
{
    public class DeleteSlideRequestHandler : IRequestHandler<DeleteSlideRequest, Response<Slide>>
    {
        private readonly DBContext _dbContext;
        private readonly ISlideRepository _slideRepository;
        public DeleteSlideRequestHandler(DBContext dbContext, ISlideRepository slideRepository)
        {
            _dbContext = dbContext;
            _slideRepository = slideRepository;
        }

        public async Task<Response<Slide>> Handle(DeleteSlideRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var slide = _dbContext.Slides.FirstOrDefault(x => x.Id == request.Id);
                if (slide == null)
                {
                    throw new Exception("Id not found");
                }
                var response = await _slideRepository.Delete(request.Id);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while Delete the Slide.",ex);
            }
        }
    }
}
