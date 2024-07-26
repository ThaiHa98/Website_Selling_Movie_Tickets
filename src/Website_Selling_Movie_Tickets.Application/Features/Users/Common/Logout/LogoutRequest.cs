using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.Logout
{
    public class LogoutRequest : IRequest<string>
    {
        public int Id { get; set; }
    }
}
