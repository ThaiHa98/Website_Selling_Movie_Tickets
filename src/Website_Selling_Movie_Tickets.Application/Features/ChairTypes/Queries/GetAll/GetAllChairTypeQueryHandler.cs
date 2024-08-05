using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Queries.GetAll
{
    public class GetAllChairTypeQueryHandler : IRequestHandler<GetAllChairTypeQuery,Response<List<ChairType>>>
    {
        private readonly IChairTypeRepository _chairTypeRepository;
        public GetAllChairTypeQueryHandler(IChairTypeRepository chairTypeRepository)
        {
            _chairTypeRepository = chairTypeRepository;
        }
        public async Task<Response<List<ChairType>>> Handle(GetAllChairTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _chairTypeRepository.GetAll();
                if (response == null || !response.Success)
                {
                    return new Response<List<ChairType>>
                    {
                        Success = false,
                        Message = response?.Message ?? "Failed to retrieve chair types."
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new Response<List<ChairType>>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving chair types: {ex.Message}"
                };
            }
        }
    }
}
