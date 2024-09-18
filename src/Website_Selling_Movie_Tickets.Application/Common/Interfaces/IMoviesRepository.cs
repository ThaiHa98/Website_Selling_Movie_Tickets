using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Shared.DTOs.Booking;
using Shared.DTOs.MoviesView;
using Shared.DTOs.SearchStatusMovies;
using Shared.DTOs.SubtitleTables;
using Shared.DTOs.SubtitleTableTimeSlots;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface IMoviesRepository
    {
        Task<List<Movie>> GetAll();
        Task<Pagination<Movie>> GetPagination(int pageIndex, int pageSize);
        Task<List<TheaterViewModel>> GetPremiere(int id, DateTime premiere);
        Task<Response<Movie>> Create(Movie movie);
        Task<string> Update(Movie movie);
        Task<bool> Delete(Movie movie);
        Task<Movie> SearchByKeyAsync(string key);
        Task<byte[]> GetMovieImageBytes(int id);
        Task<List<MoviesViewModel>> SearchStatusAsync(string status);
        Task<List<MoviesViewModel>> SearchIronfilmreleased(string status);
        Task<MoviesViewModel> MoviesDetails(int id);
        Task<List<string>> GetTheaterAddressesByMovieId(int id);
        Task<List<TheaterViewModel>> GetTheaterDetails(int id, string address);
        Task<List<SubtitleTablesModel>> GetSubtitleTables(int movieId);
        Task<List<BookingModel>> GetBooking(int movie_Id, string theater_Address, int subtitleTable_Id);
        Task<string> LoadUserImage(int id);
        Task<List<SubtitleTableTimeSlotsModel>> GetTimeSlot(int movieId, string nameSubtitleTable);
    }
}
