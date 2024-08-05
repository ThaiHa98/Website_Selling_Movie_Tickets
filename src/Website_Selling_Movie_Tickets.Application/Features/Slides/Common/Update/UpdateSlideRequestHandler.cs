using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Update
{
    public class UpdateSlideRequestHandler : IRequestHandler<UpdateSlidesRequest, Response<Slide>>
    {
        private readonly ISlideRepository _slideRepository;
        private readonly DBContext _dbContext;
        public UpdateSlideRequestHandler(ISlideRepository slideRepository, DBContext dBContext) 
        {
            _dbContext = dBContext;
            _slideRepository = slideRepository;
        }
        public async Task<Response<Slide>> Handle(UpdateSlidesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var slide = _dbContext.Slides.FirstOrDefault(x => x.Id == request.Id);
                if (slide == null)
                {
                    throw new Exception("Id not found");
                }
                slide.Url = request.Url;
                slide.Status = request.Status;
                var response = await _slideRepository.Update(slide);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while updating the Slide.", ex);
            }
        }
    }
}
