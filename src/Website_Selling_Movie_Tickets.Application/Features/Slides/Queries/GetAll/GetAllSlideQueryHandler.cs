using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetAll
{
    public class GetAllSlideQueryHandler : IRequestHandler<GetAllSlidesQuery, List<Slide>>
    {
        private readonly ISlideRepository _slideRepository;
        public GetAllSlideQueryHandler(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public async Task<List<Slide>> Handle(GetAllSlidesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var slides = await _slideRepository.GetAll();
                return slides;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving slides.", ex);
            }
        }
    }
}
