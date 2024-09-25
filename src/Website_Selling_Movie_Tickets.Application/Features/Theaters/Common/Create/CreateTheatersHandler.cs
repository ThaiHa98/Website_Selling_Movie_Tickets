using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Create
{
    public class CreateTheatersHandler : IRequestHandler<CreateTheatersRequest, Theater>
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly DBContext _dbContext;

        public CreateTheatersHandler(ITheaterRepository theaterRepository, DBContext dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(theaterRepository));
            _theaterRepository = theaterRepository ?? throw new ArgumentNullException(nameof(theaterRepository));
        }

        public async Task<Theater> Handle(CreateTheatersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var subtitleTableIds = new List<int>();
                var ScreeningRoom_Ids = new List<int>();
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The request was not fully entered into the data fields");
                }
                foreach (var ScreeningRoom_Id in request.ScreeningRoom_Id)
                {
                    var ScreeningRoom = await _dbContext.ScreeningRooms.FirstOrDefaultAsync(x => x.Id == ScreeningRoom_Id);
                    if(ScreeningRoom == null)
                    {
                        throw new Exception($"Id {ScreeningRoom_Id} not found");
                    }
                    ScreeningRoom_Ids.Add(ScreeningRoom.Id);
                }
                // Kiểm tra các TimeSlot_Id
                foreach (var subtitleTableId in request.SubtitleTable_Id)
                {
                    var subtitleTable = await _dbContext.SubtitleTables.FirstOrDefaultAsync(x => x.Id == subtitleTableId);
                    if (subtitleTable == null) 
                    {
                        throw new Exception($"Id {subtitleTableId} not found");
                    }
                    subtitleTableIds.Add(subtitleTable.Id);
                }
                // Tạo một theater với tất cả TimeSlot_Id
                var theater = new Theater
                {
                    Name = request.Name,
                    Address = request.Address,
                    Date = request.Date,
                    SubtitleTable_Id = string.Join(",", subtitleTableIds),
                    ScreeningRoom_Id = string.Join(",", ScreeningRoom_Ids)
                };
                var response = await _theaterRepository.Create(theater);

                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response.Data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the theater.", ex);
            }
        }
    }
}
