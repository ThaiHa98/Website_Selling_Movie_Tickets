using FluentValidation;
using MediatR;
using Shared.DTOs.User;
using System;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.CreateAdmin
{
    public class CreateUserHandler : IRequestHandler<CreateAdminUsersRequest, UserModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateAdminUsersRequest> _validator;

        public CreateUserHandler(IUserRepository userRepository, IValidator<CreateAdminUsersRequest> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserModel> Handle(CreateAdminUsersRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Yêu cầu chưa được nhập đầy đủ vào các trường dữ liệu");
            }

            var validatorResult = await _validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                var errors = string.Join(", ", validatorResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(validatorResult.Errors);
            }

            try
            {
                var response = await _userRepository.AddAdmin(request.UserModel);
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
