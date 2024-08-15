using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.SubtitleTable;
using Shared.DTOs.Theater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetById
{
    public class GetByIdTheatersHandler : IRequestHandler<GetByIdTheatersQuery, TheaterModel>
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly DBContext _dbContext;

        public GetByIdTheatersHandler(ITheaterRepository theaterRepository, DBContext dbContext)
        {
            _theaterRepository = theaterRepository;
            _dbContext = dbContext;
        }

        public async Task<TheaterModel> Handle(GetByIdTheatersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy thông tin từ Theaters
                var theater = await _dbContext.Theaters
                    .Where(st => st.Id == request.Id)
                    .Select(st => new
                    {
                        st.Id,
                        st.Name,
                        st.Address,
                        st.Date,
                        SubtitleTableIds = st.SubtitleTable_Id // Giả sử đây là chuỗi các ID được phân cách bằng dấu phẩy
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (theater == null)
                {
                    // Nếu không tìm thấy theater, trả về giá trị null
                    return null;
                }

                // Xử lý SubtitleTableIds
                var subtitleTableIds = theater.SubtitleTableIds.Split(',').Select(int.Parse).ToList();

                // Lấy danh sách SubtitleTable tương ứng
                var subtitleTables = await _dbContext.SubtitleTables
                    .Where(x => subtitleTableIds.Contains(x.Id))
                    .Select(x => new Shared.DTOs.Theater.SubtitleTableModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TimeSlot_Id = x.TimeSlot_Id,
                    })
                    .ToListAsync();

                // Tạo đối tượng TheaterModel để trả về
                var theaterModel = new TheaterModel
                {
                    Id = theater.Id,
                    Name = theater.Name,
                    Address = theater.Address,
                    Date = theater.Date,
                    SubtitleTable = subtitleTables
                };

                return theaterModel;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và ném ngoại lệ
                throw new ApplicationException("An error occurred while fetching the theater by ID.", ex);
            }
        }
    }
}
