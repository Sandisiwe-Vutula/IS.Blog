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
    public class PostService : IPostService
    {
        private readonly BlogDBContext _context;

        public PostService(BlogDBContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
        public async Task<Post> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> UpdatePostAsync(int id, Post updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return new Post { Title = "Error", Content = "Post not found" };
            }
            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;
            post.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return new Post { Title = "Error", Content = "Post not found" };
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> LikePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                 return new Post { Title = "Error", Content = "Post not found" };
            }
            post.Likes++;
            await _context.SaveChangesAsync();
            return post;
        }
    }

}
