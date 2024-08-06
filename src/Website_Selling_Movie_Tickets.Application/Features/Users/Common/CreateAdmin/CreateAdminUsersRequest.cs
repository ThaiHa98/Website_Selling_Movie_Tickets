using MediatR;
using Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.CreateAdmin
{
    public class CreateAdminUsersRequest : IRequest<UserModel>
    {
        public UserModel UserModel { get; set; }
        public CreateAdminUsersRequest(UserModel userModel)
        {
            UserModel = userModel ?? throw new ArgumentNullException(nameof(userModel));
        }
    }
}
