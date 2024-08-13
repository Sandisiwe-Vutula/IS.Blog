using IS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Services.Contracts
{
    public interface IUserService
    {
        //Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
