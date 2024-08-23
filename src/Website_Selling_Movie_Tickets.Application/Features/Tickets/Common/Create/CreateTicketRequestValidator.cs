using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Create
{
    public class CreateTicketRequestValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketRequestValidator() 
        {
            RuleFor(request => request.User_Id)
                .NotEmpty().WithMessage("User_Id cannot be null");

            RuleFor(request => request.Movies_Id)
                .NotEmpty().WithMessage("Movies_Id cannot be null");

            RuleFor(request => request.TimeSlot_Id)
                .NotEmpty().WithMessage("TimeSlot_Id cannot be null");

            RuleFor(request => request.ChairType_Id)
                .NotEmpty().WithMessage("ChairType_Id cannot be null");

            RuleFor(request => request.ScreeningRoom_Id)
                .NotEmpty().WithMessage("ScreeningRoom_Id cannot be null");

            RuleFor(request => request.Theaters_Id)
                .NotEmpty().WithMessage("Theaters_Id cannot be null");
        }
    }
}
