using FluentValidation;
using Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Create;

namespace Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common
{
    public class CreateScreeningRoomRequestValidator : AbstractValidator<CreateScreeningRoomRequest>
    {
        public CreateScreeningRoomRequestValidator()
        {
            RuleFor(request => request.ScreeningRoomModel)
                .NotNull().WithMessage("ScreeningRoomModel cannot be null");

            RuleFor(request => request.ScreeningRoomModel.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(request => request.ScreeningRoomModel.Numberofseats)
                .GreaterThan(0).WithMessage("Number of seats must be greater than 0");

            RuleFor(request => request.ScreeningRoomModel.NumberOfRegularSeat)
                .GreaterThanOrEqualTo(0).WithMessage("Number of regular seats cannot be negative");

            RuleFor(request => request.ScreeningRoomModel.NumberOfVIPSeat)
                .GreaterThanOrEqualTo(0).WithMessage("Number of VIP seats cannot be negative");

            RuleFor(request => request.ScreeningRoomModel.NumberOfLoveBoxes)
                .GreaterThanOrEqualTo(0).WithMessage("Number of love boxes cannot be negative");
        }
    }
}
