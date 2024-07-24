using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace TinTuc.Application.Helper
{
    public class Token
    {
        private readonly DBContext _dbContext;
        private readonly IConfiguration _configuration;

        public Token(DBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        //Create token
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token256").Value!));//sử dung khóa 256 bit
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime now = DateTime.Now;
            int expirationMinutes = 60; //thơi gian hết hạn
            DateTime expiration = now.AddMinutes(expirationMinutes); //tính thời gian hết hạn

            var token = new JwtSecurityToken(claims: claims, expires: expiration,
                signingCredentials: cred, issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"]);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        // Phương thức xác thực token
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Token256"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
            };
            try
            {
                // ValidateToken ném ra ngoại lệ nếu mã thông báo không hợp lệ
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                // Nếu không có lỗi ném ra, token hợp lệ
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi xác thực token
                return false;
            }
        }

        // Phương thức lấy ClaimsPrincipal từ token
        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Token256"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
            };
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return principal;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when validating token", ex);
            }
        }
    }
}
