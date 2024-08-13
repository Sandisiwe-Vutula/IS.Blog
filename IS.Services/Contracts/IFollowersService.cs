using IS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Services.Contracts
{
    public interface IFollowersService
    {
        Task<Followers> GetFollowerAsync(int id);
        Task UpdateFollowerAsync(Followers follower);
    }
}
