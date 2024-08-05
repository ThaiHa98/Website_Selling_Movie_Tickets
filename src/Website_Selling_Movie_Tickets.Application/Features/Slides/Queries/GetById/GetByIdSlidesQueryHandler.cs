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

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetById
{
    public class GetByIdSlidesQueryHandler : IRequestHandler<GetByIdSlidesQuery,Slide>
    {
        private readonly ISlideRepository _slideRepository;
        private readonly DBContext _dbContext;
        public GetByIdSlidesQueryHandler(ISlideRepository slideRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _slideRepository = slideRepository;
        }

        public async Task<Slide> Handle(GetByIdSlidesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var slide = _dbContext.Slides.FirstOrDefault(x => x.Id == request.Id);
                if (slide == null)
                {
                    throw new Exception("Id not found");
                }
                var result = await _slideRepository.GetById(request.Id);
                return result;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while retrieving slides.", ex);
            }
        }
    }
}
