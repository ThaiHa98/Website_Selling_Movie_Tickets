﻿using MediatR;
using Shared.DTOs.SubtitleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetSubtitleTable
{
    public class GetSubtitleTableQuery : IRequest<List<SubtitleTablesModel>>
    {
        public int Id { get; set; }
    }
}
