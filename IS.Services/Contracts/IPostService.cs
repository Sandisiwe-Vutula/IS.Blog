using IS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Services.Contracts
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        //Task<Post> GetPostAsync(long id);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(int id, Post updatedPost);
        Task<Post> DeletePostAsync(int id);
        Task<Post> LikePostAsync(int id);
    }
}
