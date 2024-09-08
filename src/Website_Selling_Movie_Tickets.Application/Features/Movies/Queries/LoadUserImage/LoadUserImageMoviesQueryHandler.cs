using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.LoadUserImage
{
    public class LoadUserImageMoviesQueryHandler : IRequestHandler<LoadUserImageMoviesQuery, string>
    {
        private readonly IMoviesRepository _moviesRepository;

        public LoadUserImageMoviesQueryHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<string> Handle(LoadUserImageMoviesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var imageUrl = await _moviesRepository.LoadUserImage(request.Id);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Không tìm thấy dữ liệu hình ảnh cho bộ phim với ID: " + request.Id);
                }
                return imageUrl;
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi xử lý yêu cầu LoadUserImageMoviesQuery.", ex);
            }
        }
    }
}
