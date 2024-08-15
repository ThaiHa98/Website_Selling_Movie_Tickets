using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.SubtitleTable;
using Shared.SeedWork;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Queries.GetById
{
    internal class GetByIdSubtitleTablesQueryHandler : IRequestHandler<GetByIdSubtitleTablesQuery, Response<SubtitleTableModel>>
    {
        private readonly DBContext _dbContext;

        public GetByIdSubtitleTablesQueryHandler(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<SubtitleTableModel>> Handle(GetByIdSubtitleTablesQuery request, CancellationToken cancellationToken)
        {
            // Lấy thông tin từ SubtitleTable
            var subtitleTable = await _dbContext.SubtitleTables
                .Where(st => st.Id == request.Id)
                .Select(st => new
                {
                    st.Id,
                    st.Name,
                    TimeSlotIds = st.TimeSlot_Id // Giả sử đây là chuỗi các ID được phân cách bằng khoảng trắng
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (subtitleTable == null)
            {
                return new Response<SubtitleTableModel>
                {
                    Success = false,
                    Message = "Không tìm thấy SubtitleTable với ID đã cho."
                };
            }

            // Xử lý TimeSlotIds trong bộ nhớ
            var timeSlotIds = subtitleTable.TimeSlotIds.Split(' ').Select(int.Parse).ToList();

            // Lấy thông tin TimeSlot dựa trên các TimeSlot_Id
            var timeSlots = await _dbContext.TimeSlots
                .Where(ts => timeSlotIds.Contains(ts.Id))
                .Select(ts => new TimeSlotDetails
                {
                    Id = ts.Id,
                    StartTime = ts.StartTime,
                    EndTime = ts.EndTime,
                    Date = ts.Date
                })
                .ToListAsync(cancellationToken);

            // Tạo đối tượng SubtitleTableModel
            var result = new SubtitleTableModel
            {
                Id = subtitleTable.Id,
                Name = subtitleTable.Name,
                TimeSlots = timeSlots
            };

            return new Response<SubtitleTableModel>
            {
                Success = true,
                Data = result,
                Message = "Lấy dữ liệu thành công."
            };
        }
    }
}
