using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Common.Update
{
    public class UpdateSubtitleTablesRequestHandler : IRequestHandler<UpdateSubtitleTablesRequest, SubtitleTable>
    {
        private readonly DBContext _dbContext;

        public UpdateSubtitleTablesRequestHandler(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubtitleTable> Handle(UpdateSubtitleTablesRequest request, CancellationToken cancellationToken)
        {
            // Truy xuất đối tượng SubtitleTable từ cơ sở dữ liệu
            var subtitleTable = await _dbContext.SubtitleTables
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (subtitleTable == null)
            {
                throw new Exception("SubtitleTable not found");
            }
            try
            {
                // Cập nhật thuộc tính TimeSlot_Id
                subtitleTable.TimeSlot_Id = string.Join(" ", request.TimeSlot_Id); // Chuyển List<int> thành chuỗi

                // Lưu các thay đổi vào cơ sở dữ liệu
                _dbContext.SubtitleTables.Update(subtitleTable);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return subtitleTable;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while Update the SubtitleTables.", ex);
            }
        }
    }
}
