﻿using MediatR;
using Shared.DTOs.SubtitleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetSubtitleTable
{
    public class GetSubtitleTableQueryHandler : IRequestHandler<GetSubtitleTableQuery, List<SubtitleTablesModel>>
    {
        private readonly IMoviesRepository _moviesRepository;

        public GetSubtitleTableQueryHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<List<SubtitleTablesModel>> Handle(GetSubtitleTableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _moviesRepository.GetSubtitleTables(request.Id);
                if (result == null)
                {
                    throw new Exception("Data not found");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while processing GetSubtitleTable: {ex.Message}", ex);
            }
        }
    }
}
