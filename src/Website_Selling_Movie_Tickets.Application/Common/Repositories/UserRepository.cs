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

        public async Task<Response<UserModel>> AddAsync(UserModel userModel)
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
            var result = await _dbContext.SaveChangesAsync();
            var addedUserModel = new UserModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                Phone = user.Phone,
                DateofBirth = user.DateofBirth,
            };
            if (result > 0) 
            {
                return new Response<UserModel>
                {
                    Success = true,
                    Data = addedUserModel,
                    Message = "User has been added successfully"
                };
            }
            else
            {
                return new Response<UserModel>
                {
                    Success = false,
                    Message = "An error occurred while adding the user"
                };
            }
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

        public async Task<Response<User>> RemoveAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(user);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return new Response<User>
                {
                    Success = true,
                    Message = "User has been removed successfully"
                };
            }
            else
            {
                return new Response<User>
                {
                    Success = false,
                    Message = "Failed to remove user"
                };
            }
        }

        public async Task<Response<User>> Update(User entity)
        {
            try
            {
                _dbContext.Users.Update(entity);
                var result = await _dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return new Response<User>
                    {
                        Success = true,
                        Data = entity,
                        Message = "User has been updated successfully"
                    };
                }
                else
                {
                    return new Response<User>
                    {
                        Success = false,
                        Message = "Failed to update user"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<User>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }


        public async Task<User>GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
    }
}
