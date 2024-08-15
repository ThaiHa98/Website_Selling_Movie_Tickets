using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Application.Common.Repositories;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetImage
{
    public class GetImageSlideQueryHandler : IRequestHandler<GetImageSlideQuery,byte[]>
    {
        private readonly ISlideRepository _slideRepository;
        private readonly DBContext _dbContext;

        public GetImageSlideQueryHandler(ISlideRepository slideRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _slideRepository = slideRepository;
        }

        public async Task<byte[]> Handle(GetImageSlideQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = await _dbContext.Slides.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (movie == null)
                {
                    throw new Exception("Id not found");
                }

                // Lấy hình ảnh dưới dạng mảng byte từ repository
                var imageBytes = await _slideRepository.GetSlideImageBytes(request.Id);
                return imageBytes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while searching for the movie.", ex);
            }
        }
    }
}
