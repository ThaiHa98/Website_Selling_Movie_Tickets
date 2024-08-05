using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Queries.GetPagination
{
    public class GetPaginationChairtypeHandler : IRequestHandler<GetPaginationchairTypeQuery,Response<Pagination<ChairType>>>
    {
        private readonly IChairTypeRepository _chairTypeRepository;
        public GetPaginationChairtypeHandler(IChairTypeRepository chairTypeRepository)
        {
            _chairTypeRepository = chairTypeRepository;
        }
        public async Task<Response<Pagination<ChairType>>> Handle(GetPaginationchairTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _chairTypeRepository.GetPagination(request.pageIndex, request.pageSize);

                if (response == null || !response.Success)
                {
                    var emptyPagination = new Pagination<ChairType>(request.pageIndex, request.pageSize, 0, new List<ChairType>());
                    return new Response<Pagination<ChairType>>
                    {
                        Data = emptyPagination,
                        Success = false,
                        Message = "No data found"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new Response<Pagination<ChairType>>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occurred while retrieving chair types: {ex.Message}"
                };
            }
        }
    }
}
