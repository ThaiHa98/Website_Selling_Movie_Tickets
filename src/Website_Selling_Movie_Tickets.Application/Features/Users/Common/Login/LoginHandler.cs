using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using TinTuc.Application.Helper;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest,string>
    {
        private readonly IUserRepository _userRepository;
        private readonly DBContext _dbContext;
        private readonly Token _token;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginHandler(IUserRepository userRepository, DBContext dbContext, Token token, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _token = token;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "The request has not been filled in with enough data");
                }
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == request.Email);
                if (user == null)
                {
                    throw new Exception("Email not found");
                }
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    throw new Exception("Incorrect password!");
                }
                // Tạo token cho người dùng
                string token = _token.CreateToken(user);

                var context = _httpContextAccessor.HttpContext;
                if (context != null)
                {
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.Now.AddMinutes(60)
                    };
                    context.Response.Cookies.Append("authenticationToken", token, cookieOptions);
                }
                return await Task.FromResult(token);
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while logging in", ex);
            }
        }
    }
}
