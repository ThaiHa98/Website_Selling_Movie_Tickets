﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Actor;
using Shared.DTOs.ScreeningRoom;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class ScreeningRoomRepository : IScreeningRoomRepository
    {
        private readonly DBContext _dbContext;
        public ScreeningRoomRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Response<ScreeningRoomModel>> AddAsync(ScreeningRoomModel entity)
        {
            var screeningRoom = new ScreeningRoom
            {
                Name = entity.Name,
                Numberofseats = entity.Numberofseats,
                NumberOfRegularSeat = entity.NumberOfRegularSeat,
                NumberOfVIPSeat = entity.NumberOfVIPSeat,
                NumberOfLoveBoxes = entity.NumberOfLoveBoxes,
                Status = StatusScreenigRoom.Available,
            };
            _dbContext.ScreeningRooms.Add(screeningRoom);
            var result = await _dbContext.SaveChangesAsync();
            var screeningRoomModel = new ScreeningRoomModel
            {
                Name = screeningRoom.Name,
                Numberofseats = screeningRoom.Numberofseats,
                NumberOfRegularSeat = screeningRoom.NumberOfRegularSeat,
                NumberOfVIPSeat = screeningRoom.NumberOfVIPSeat,
                NumberOfLoveBoxes = screeningRoom.NumberOfLoveBoxes,
            };
            if (result > 0)
            {
                return new Response<ScreeningRoomModel>
                {
                    Success = true,
                    Data = screeningRoomModel,
                    Message = "ScreeningRoom has been added successfully."
                };
            }
            else
            {
                return new Response<ScreeningRoomModel>
                {
                    Success = false,
                    Message = "An error occurred while adding the screeningRoom."
                };
            }
        }

        public async Task<ScreeningRoomModeSeatl> GetAllScreeningMovieId(int movie_Id)
        {
            try
            {
                // Lấy movie theo movie_Id
                var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == movie_Id);
                if (movie == null)
                {
                    throw new Exception("Movie_Id not found");
                }

                // Lấy ScreeningRoom_Id từ movie
                var screeningRoomId = movie.ScreeningRoom_Id;

                // Lấy thông tin ScreeningRoom
                var screeningRoom = _dbContext.ScreeningRooms.FirstOrDefault(sr => sr.Id == screeningRoomId);
                if (screeningRoom == null)
                {
                    throw new Exception("ScreeningRoom not found");
                }

                // Lấy danh sách các ghế thuộc ScreeningRoom_Id
                var domainSeats = _dbContext.Seats.Where(seat => seat.ScreeningRoom_Id == screeningRoomId).ToList();

                // Ánh xạ từ Domain.Seat sang DTO.Seat
                var dtoSeats = domainSeats.Select(seat => new Shared.DTOs.ScreeningRoom.Seat
                {
                    Id = seat.Id,
                    ScreeningRoom_Id = seat.ScreeningRoom_Id,
                    ChairType_Id = seat.ChairType_Id,
                    Row = seat.Row,
                    Number = seat.Number
                }).ToList();

                // Tạo đối tượng ScreeningRoomModeSeatl
                var screeningRoomWithSeats = new ScreeningRoomModeSeatl
                {
                    Id = screeningRoom.Id,
                    Name = screeningRoom.Name,
                    Numberofseats = screeningRoom.Numberofseats,
                    NumberOfRegularSeat = screeningRoom.NumberOfRegularSeat,
                    NumberOfVIPSeat = screeningRoom.NumberOfVIPSeat,
                    NumberOfLoveBoxes = screeningRoom.NumberOfLoveBoxes,
                    Seats = dtoSeats // Gán danh sách ghế đã ánh xạ
                };

                return screeningRoomWithSeats;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving screening room and seats: {ex.Message}");
            }
        }


        public async Task<Response<ScreeningRoom>> GetById(int Id)
        {
            var result = await _dbContext.ScreeningRooms.FirstAsync(r => r.Id == Id);
            if(result != null)
            {
                return new Response<ScreeningRoom>
                {
                    Data = result,
                    Success = true,
                    Message = "Successfully retrieved the screening room."
                };
            }
            else
            {
                return new Response<ScreeningRoom>
                {
                    Success = true,
                    Message = "Screening room not found."
                };
            }
        }

        public async Task<Pagination<ScreeningRoom>> GetPagination(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.ScreeningRooms.CountAsync();
            var items = _dbContext.ScreeningRooms
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new Pagination<ScreeningRoom>(pageIndex, pageSize, totalRecords ,items);
        }

        public async Task<bool> RemoveAsync(ScreeningRoom screeningRoom)
        {
            _dbContext.Remove(screeningRoom);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Pagination<ScreeningRoom>> SearchByKeyAsync(string key, int pageIndex, int pageSize)
        {
            var query = _dbContext.ScreeningRooms.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(sr => sr.Name.Contains(key));
            }

            var totalItems = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new Pagination<ScreeningRoom>(pageIndex, pageSize, totalItems, items);
        }


        public async Task<string> UpdateAsync(ScreeningRoom entity)
        {
            _dbContext.ScreeningRooms.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return "Update Successfully";
            }
            else
            {
                return "Update Failed";
            }
        }
    }
}
