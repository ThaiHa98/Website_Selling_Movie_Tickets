﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Genres.Common.Create
{
    public class CreateUserRequestValidator : AbstractValidator<CreateGenreRequest>
    {
       public CreateUserRequestValidator()
       {
            RuleFor(x => x.Name)
                .NotEmpty();
       }
    }
}
