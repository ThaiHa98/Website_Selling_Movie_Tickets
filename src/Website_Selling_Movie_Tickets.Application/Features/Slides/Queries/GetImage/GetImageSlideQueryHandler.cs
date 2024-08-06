using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetImage
{
    public class GetImageSlideQueryHandler : IRequestHandler<GetImageSlideQuery, byte[]>
    {
        private readonly ISlideRepository _slideRepository;

        public GetImageSlideQueryHandler(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public async Task<byte[]> Handle(GetImageSlideQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var imageBytes = await _slideRepository.GetSlideImageBytes(request.Id);

                if (imageBytes == null || imageBytes.Length == 0)
                {
                    throw new Exception("Image not found or is empty.");
                }

                return imageBytes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while retrieving the image.", ex);
            }
        }
    }
}
