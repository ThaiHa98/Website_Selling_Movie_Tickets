using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Common.Create
{
    public class CreateSubtitleTableRequestValidator : AbstractValidator<CreateSubtitleTableRequest>
    {
        public CreateSubtitleTableRequestValidator() 
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(request => request.TimeSlot_Id)
                .NotEmpty().WithMessage("TimeSlot_Id cannot be null");
        }
    }
}
