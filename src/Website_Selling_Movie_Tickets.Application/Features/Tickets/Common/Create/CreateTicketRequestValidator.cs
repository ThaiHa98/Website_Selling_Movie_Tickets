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
            RuleFor(request => request.TicketModel.User_Id)
                .NotEmpty().WithMessage("User_Id cannot be null");

            RuleFor(request => request.TicketModel.Movies_Id)
                .NotEmpty().WithMessage("Movies_Id cannot be null");

            RuleFor(request => request.TicketModel.TimeSlot_Id)
                .NotEmpty().WithMessage("TimeSlot_Id cannot be null");

            RuleFor(request => request.TicketModel.ChairType_Id)
                .NotEmpty().WithMessage("ChairType_Id cannot be null");

            RuleFor(request => request.TicketModel.ScreeningRoom_Id)
                .NotEmpty().WithMessage("ScreeningRoom_Id cannot be null");

            RuleFor(request => request.TicketModel.Theaters_Id)
                .NotEmpty().WithMessage("Theaters_Id cannot be null");
        }
    }
}
