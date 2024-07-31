using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.Users.Common.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly DBContext _dbContext;
        public UpdateUserHandler(IUserRepository userRepository, DBContext dbContext)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        public async Task<User> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Tìm người dùng bất đồng bộ bằng FindAsync
                var user = await _dbContext.Users.FindAsync(request.Id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy Id");
                }

                // Cập nhật các thuộc tính của người dùng
                user.Email = request.Email;
                user.Address = request.Address;
                user.Phone = request.Phone;

                // Cập nhật người dùng và nhận kết quả
                var response = await _userRepository.Update(user);

                if (response.Success)
                {
                    return response.Data; // Trả về người dùng đã được cập nhật
                }
                else
                {
                    throw new Exception(response.Message); // Ném ngoại lệ nếu cập nhật không thành công
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật người dùng: {ex.Message}", ex);
            }
        }
    }
}
