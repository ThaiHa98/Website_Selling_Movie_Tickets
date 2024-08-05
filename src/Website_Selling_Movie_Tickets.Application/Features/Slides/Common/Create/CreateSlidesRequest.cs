using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Create
{
    public class CreateSlidesRequest : IRequest<Response<Slide>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public IFormFile Image { get; set; }
        public int Sort { get; set; }
        public StatusSlide Status { get; set; }
    }
}
