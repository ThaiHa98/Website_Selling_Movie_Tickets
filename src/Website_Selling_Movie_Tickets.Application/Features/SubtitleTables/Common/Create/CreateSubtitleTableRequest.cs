using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Common.Create
{
    public class CreateSubtitleTableRequest : IRequest<Response<List<SubtitleTable>>>
    {
        public string Name { get; set; }
        public List<int> TimeSlot_Id { get; set; }
    }
}
