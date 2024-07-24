using Microsoft.EntityFrameworkCore;
using Shared.DTOs.User;
using Shared.SeedWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Common.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _dbContext;

        public UserRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> AddAsync(UserModel userModel)
        {
            var user = new User
            {
                Name = userModel.Name,
                Email = userModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password),
                Address = userModel.Address,
                Phone = userModel.Phone,
                DateofBirth = userModel.DateofBirth,
                Roles = Roles.User,
                Create = DateTime.Now
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            var addedUserModel = new UserModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                Phone = user.Phone,
                DateofBirth = user.DateofBirth,
            };

            return addedUserModel;
        }

        public async Task<IList<User>> GetAll()
        {
            var user = await _dbContext.Users.ToListAsync();
            return user;
        }

        public async Task<Pagination<User>> GetAllUser(int pageIndex, int pageSize)
        {
            var totalRecords = await _dbContext.Users.CountAsync();
            var items = await _dbContext.Users
                                      .Skip((pageIndex - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            return new Pagination<User>(pageIndex, pageSize, totalRecords, items);
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }


        Task<User> IUserRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
