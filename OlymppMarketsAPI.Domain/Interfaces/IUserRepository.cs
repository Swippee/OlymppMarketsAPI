using Microsoft.EntityFrameworkCore.Storage;
using OlymppMarketsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlymppMarketsAPI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}
