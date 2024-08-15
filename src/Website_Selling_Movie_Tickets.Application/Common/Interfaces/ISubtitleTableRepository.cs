using Shared.DTOs.SearchStatusMovies;
using Shared.DTOs.SubtitleTable;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface ISubtitleTableRepository
    {
        Task<List<SubtitleTableModel>> GetAll();
        Task<Pagination<SubtitleTable>> GetPagination(int pageIndex, int pageSize);
        Task<SubtitleTableModel> GetById(int id);
        Task<Response<SubtitleTable>> Create(SubtitleTable subtitleTable);
        Task<string> Update(SubtitleTable subtitleTable);
        Task<bool> DeleteAsync(int id);
        Task<SubtitleTable> SearchByKeyAsync(string key);
        Task<List<SubtitleTable>> SearchStatusAsync(string status);
    }
}
