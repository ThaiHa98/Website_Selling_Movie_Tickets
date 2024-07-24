using MediatR;
using Shared.DTOs.User;
using System;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.Create
{
    public class CreateUserRequest : IRequest<UserModel>
    {
        public UserModel UserModel { get; }

        public CreateUserRequest(UserModel userModel)
        {
            UserModel = userModel ?? throw new ArgumentNullException(nameof(userModel));
        }
    }
}
