using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetPagination
{
    public class GetPaginationSlideQueryHandler : IRequestHandler<GetPaginationSlideQuery, Pagination<Slide>>
    {
        private readonly ISlideRepository _slideRepository;
        public GetPaginationSlideQueryHandler(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public async Task<Pagination<Slide>> Handle(GetPaginationSlideQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The request was not fully entered into the data fields");
                }

                var slides = await _slideRepository.GetPagination(request.pageIndex, request.pageSize);
                return slides;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving paginated slides.", ex);
            }
        }
    }
}
