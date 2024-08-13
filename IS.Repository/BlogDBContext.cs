using IS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Repository
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<LiveVideo> LiveVideos { get; set; }
        public DbSet<Followers> Followers { get; set; }
        //public DbSet<Reply> Replies { get; set; }
        public DbSet<Request> Requests { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Followers>()
               .HasOne(f => f.User)
               .WithMany(u => u.FollowedUsers)
               .HasForeignKey(f => f.UserID);
        }
    }
}
