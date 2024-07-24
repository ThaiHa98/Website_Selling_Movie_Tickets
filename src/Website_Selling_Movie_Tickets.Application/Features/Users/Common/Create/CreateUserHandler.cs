using MediatR;
using Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, UserModel>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserHandler(IUserRepository userRepository) 
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<UserModel> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request == null)
                {
                    throw new Exception("The request was not fully entered into the data fields");
                }
                var user = await _userRepository.AddAsync(request.UserModel);
                var userModel = new UserModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Address = user.Address,
                    Phone = user.Phone,
                    DateofBirth = user.DateofBirth
                };
                return userModel;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the user", ex);
            }
        }
    }
}
