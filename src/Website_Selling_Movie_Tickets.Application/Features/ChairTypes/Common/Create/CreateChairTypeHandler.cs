using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Create
{
    public class CreateChairTypeHandler : IRequestHandler<CreateChairTypeRequest, ChairType>
    {
        private readonly IChairTypeRepository _chairTypeRepository;
        private readonly DBContext _dbContext;
        public CreateChairTypeHandler(IChairTypeRepository chairTypeRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _chairTypeRepository = chairTypeRepository;
        }
        public async Task<ChairType> Handle(CreateChairTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var screeningRoom = _dbContext.ScreeningRooms.FirstOrDefault(x => x.Id == request.ScreeningRoom_Id);
                if (screeningRoom == null)
                {
                    throw new Exception("Id not found");
                }
                var chairType = new ChairType
                {
                    Name = request.Name,
                    Price = request.Price,
                    ScreeningRoom_Id = screeningRoom.Id,
                };
                var response = await _chairTypeRepository.AddAsync(chairType);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response.Data;
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while creating the theater.",ex);
            }
        }
    }
}
