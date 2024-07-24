using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.Create
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.UserModel.Name)
                .NotEmpty();

            RuleFor(x => x.UserModel.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.UserModel.Password)
                .NotEmpty();

            RuleFor(x => x.UserModel.Address)
                .NotEmpty();

            RuleFor(x => x.UserModel.Phone)
                .Matches(@"^\d{10}$")
                .WithMessage("Invalid phone number format.");
        }
    }
}
