using Shared.DTOs.Actor;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface IScreeningRoomRepository
    {
        Task<List<ScreeningRoom>> GetAll(int movie_Id);
        Task<Pagination<ScreeningRoom>> GetPagination(int pageIndex, int pageSize);
        Task<Response<ScreeningRoom>> GetById(int Id);
        Task<Response<ScreeningRoomModel>> AddAsync(ScreeningRoomModel entity);
        Task<string> UpdateAsync(ScreeningRoom entity);
        Task<bool> RemoveAsync(ScreeningRoom screeningRoom);
        Task<Pagination<ScreeningRoom>> SearchByKeyAsync(string key, int pageIndex, int pageSize);
    }
}
