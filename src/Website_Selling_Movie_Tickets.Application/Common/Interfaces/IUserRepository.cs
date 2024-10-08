﻿using Shared.DTOs.User;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAll();
        Task<Pagination<User>> GetAllUser(int pageIndex, int pageSize);
        Task<Response<UserModel>> AddAsync(UserModel entity);
        Task<User> GetByIdAsync(int id);
        Task<Response<User>> RemoveAsync(int id);
        Task<Response<User>> Update(User entity);
        Task<Response<UserModel>> AddAdmin(UserModel entity);


    }
}
