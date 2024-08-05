using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Update
{
    public class UpdateSlidesRequest : IRequest<Response<Slide>>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public StatusSlide Status { get; set; }
    }
}
