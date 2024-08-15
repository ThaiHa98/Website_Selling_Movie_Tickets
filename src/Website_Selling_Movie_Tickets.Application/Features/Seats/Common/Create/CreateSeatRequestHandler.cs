using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Seats.Common.Create
{
    public class CreateSeatRequestHandler : IRequestHandler<CreateSeatRequest, Seat>
    {
        private readonly ISeatRepository _repository;
        private readonly DBContext _dbContex;
        public CreateSeatRequestHandler(ISeatRepository repository, DBContext dbContex)
        {
            _repository = repository;
            _dbContex = dbContex;
        }

        public async Task<Seat> Handle(CreateSeatRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var screeningRoom = await _dbContex.ScreeningRooms.FirstOrDefaultAsync(x => x.Id == request.ScreeningRoom_Id);
                if (screeningRoom == null)
                {
                    throw new Exception("ScreeningRoom Id not found");
                }

                var chairType = await _dbContex.ChairTypes.FirstOrDefaultAsync(x => x.Id == request.ChairType_Id);
                if (chairType == null)
                {
                    throw new Exception("ChairType Id not found");
                }

                var seat = new Seat
                {
                    ScreeningRoom_Id = screeningRoom.Id,
                    ChairType_Id = chairType.Id,
                    Row = request.Row,
                    Number = request.Number,
                    Status = StatusSeat.Available,
                };

                var result = await _repository.AddAsync(seat);

                // Kiểm tra kết quả và xử lý
                if (!result.Success)
                {
                    throw new Exception(result.Message);
                }

                return seat; // Trả về Seat đã được thêm thành công
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the Seat.", ex);
            }
        }
    }
}
