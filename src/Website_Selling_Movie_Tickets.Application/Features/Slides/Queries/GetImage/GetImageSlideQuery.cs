﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetImage
{
    public class GetImageSlideQuery : IRequest<byte[]>
    {
        public int Id { get; set; }
    }
}
