using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.Login
{
    public class LoginRequest : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
