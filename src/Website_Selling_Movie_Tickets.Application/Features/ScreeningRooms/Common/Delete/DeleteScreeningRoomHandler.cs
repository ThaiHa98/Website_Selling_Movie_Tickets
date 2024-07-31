using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Application.Common.Repositories;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Delete
{
    public class DeleteScreeningRoomHandler : IRequestHandler<DeleteScreeningRoomRequest, bool>
    {
        private readonly IScreeningRoomRepository _screeningRoomRepository;
        private readonly DBContext _dbContext;
        public DeleteScreeningRoomHandler(IScreeningRoomRepository screeningRoomRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _screeningRoomRepository = screeningRoomRepository;
        }

        public async Task<bool> Handle(DeleteScreeningRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = _dbContext.ScreeningRooms.FirstOrDefault(x => x.Id == request.Id);
                if (movie == null)
                {
                    throw new Exception("Id not found");
                }
                var deleteMovie = await _screeningRoomRepository.RemoveAsync(movie);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while delete the genre.", ex);
            }
        }
    }
}
