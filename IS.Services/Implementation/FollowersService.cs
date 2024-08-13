using IS.Domain.Models;
using IS.Repository;
using IS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Services.Implementation
{
    public class FollowersService : IFollowersService
    {
        private readonly BlogDBContext _context;

        public FollowersService(BlogDBContext context)
        {
            _context = context;
        }

        public async Task<Followers> GetFollowerAsync(int id)
        {
            return await _context.Followers.FindAsync(id);
        }

        public async Task UpdateFollowerAsync(Followers follower)
        {
            _context.Entry(follower).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
