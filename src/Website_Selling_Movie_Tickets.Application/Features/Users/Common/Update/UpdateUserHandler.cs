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
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.Id);
                if (user == null)
                {
                    throw new Exception("Id not found");
                }
                user.Email = request.Email;
                user.Address = request.Address;
                user.Phone = request.Phone;

                var update = await _userRepository.UpdateAsync(user);
                return update;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error updating user: {ex.Message}", ex);
            }
        }
    }
}
