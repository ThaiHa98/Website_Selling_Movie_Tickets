using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Seat;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly DBContext _dbContext;
        public SeatRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Response<Seat>> AddAsync(Seat seat)
        {
            _dbContext.Seats.Add(seat);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<Seat>
                {
                    Success = true,
                    Data = seat,
                    Message = "Seat has been added successfully"
                };
            }
            else
            {
                return new Response<Seat>
                {
                    Success = false,
                    Message = "Failed to add Seat"
                };
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var entity = await _dbContext.Seats.FindAsync(Id);
            if (entity == null)
            {
                return false;
            }
            _dbContext.Seats.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<SeatModel>> GetAll()
        {
            var seats = await _dbContext.Seats
                .Select(x => new
                {
                    x.Id,
                    x.ScreeningRoom_Id,
                    x.ChairType_Id,
                    x.Row,
                    x.Number
                })
                .ToListAsync();

            if (seats == null || !seats.Any())
            {
                return new List<SeatModel>();
            }

            // Lấy danh sách các ID phòng chiếu và loại ghế duy nhất
            var screeningRoomIds = seats
                .Select(s => s.ScreeningRoom_Id)
                .Distinct()
                .ToList();

            var chairTypeIds = seats
                .Select(s => s.ChairType_Id)
                .Distinct()
                .ToList();

            var screeningRooms = await _dbContext.ScreeningRooms
                .Where(x => screeningRoomIds.Contains(x.Id))
                .Select(x => new ScreeningRoomModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Numberofseats = x.Numberofseats,
                    NumberOfRegularSeat = x.NumberOfRegularSeat,
                    NumberOfVIPSeat = x.NumberOfVIPSeat,
                    NumberOfLoveBoxes = x.NumberOfLoveBoxes,
                })
                .ToListAsync();

            var chairTypes = await _dbContext.ChairTypes
                .Where(x => chairTypeIds.Contains(x.Id))
                .Select(x => new ChairTypeModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                })
                .ToListAsync();

            var seatModels = seats.Select(seat => new SeatModel
            {
                Id = seat.Id,
                ScreeningRoom_Id = seat.ScreeningRoom_Id,
                ChairType_Id = seat.ChairType_Id,
                Row = seat.Row,
                Number = seat.Number,
                ScreeningRoom = screeningRooms.FirstOrDefault(sr => sr.Id == seat.ScreeningRoom_Id),
                ChairType = chairTypes.FirstOrDefault(ct => ct.Id == seat.ChairType_Id)
            }).ToList();

            return seatModels;
        }

        public async Task<Pagination<Seat>> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.Seats.CountAsync();
            var items = await _dbContext.Seats
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Pagination<Seat>(pageIndex, pageSize, totalRecords, items);
        }

        public async Task<SeatModel> GetSeatById(int seatId)
        {
            // Lấy thông tin ghế dựa trên seatId và trực tiếp ánh xạ vào SeatModel
            var seat = await _dbContext.Seats
                .Where(x => x.Id == seatId)
                .Select(x => new SeatModel
                {
                    Id = x.Id,
                    ScreeningRoom_Id = x.ScreeningRoom_Id,
                    ChairType_Id = x.ChairType_Id,
                    Row = x.Row,
                    Number = x.Number
                })
                .FirstOrDefaultAsync();

            if (seat == null)
            {
                return null;
            }

            // Lấy thông tin của ScreeningRoom tương ứng
            seat.ScreeningRoom = await _dbContext.ScreeningRooms
                .Where(x => x.Id == seat.ScreeningRoom_Id)
                .Select(x => new ScreeningRoomModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Numberofseats = x.Numberofseats,
                    NumberOfRegularSeat = x.NumberOfRegularSeat,
                    NumberOfVIPSeat = x.NumberOfVIPSeat,
                    NumberOfLoveBoxes = x.NumberOfLoveBoxes,
                })
                .FirstOrDefaultAsync();

            // Lấy thông tin của ChairType tương ứng
            seat.ChairType = await _dbContext.ChairTypes
                .Where(x => x.Id == seat.ChairType_Id)
                .Select(x => new ChairTypeModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                })
                .FirstOrDefaultAsync();

            return seat;
        }


        public async Task<string> UpdateAsync(int Id)
        {
            _dbContext.Update(Id);
            var result = await _dbContext.SaveChangesAsync();
            return "Update Successfully";
        }
    }
}
