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
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "Yêu cầu chưa được nhập đầy đủ vào các trường dữ liệu");
                }

                var response = await _userRepository.AddAsync(request.UserModel);
                if (!response.Success)
                {
                    throw new ApplicationException(response.Message);
                }

                return new UserModel
                {
                    Name = response.Data.Name,
                    Email = response.Data.Email,
                    Address = response.Data.Address,
                    Phone = response.Data.Phone,
                    DateofBirth = response.Data.DateofBirth
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Đã xảy ra lỗi khi tạo người dùng", ex);
            }
        }

    }
}
