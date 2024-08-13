using IS.Domain.Models;
using IS.Domain.Models.Dtos;
using IS.Repository;
using IS.Services.Contracts;
using IS.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IS.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly BlogDBContext _context;

        public PostController(IPostService postService, IUserService userService, BlogDBContext context)
        {
            _postService = postService;
            _userService = userService;
            _context = context;
        }

        [HttpPost("CreateNewPost")]
        public async Task<ActionResult<Post>> CreatePost(PostDto request)
        {
            var user = await _userService.GetUserAsync(request.Id);
            if (user.Role != RoleEnum.Admin.ToString() && user.Role != RoleEnum.User.ToString())
            {
                return Forbid();
            }
            var post = new Post
            {
                PostID = request.Id,
                Title = request.Title,
                Content = request.Content,
                CreatedDate = request.CreatedDate,
                UpdatedDate = request.UpdatedDate
            };
            return await _postService.CreatePostAsync(post);
        }

        [HttpPut("EditPost/{id}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, Post updatedPost)
        {
            var user = await _userService.GetUserAsync(id);
            if (user.Role != RoleEnum.Admin.ToString())
            {
                return Forbid();
            }
            return await _postService.UpdatePostAsync(id, updatedPost);
        }

        [HttpDelete("DeletePost/{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user.Role != RoleEnum.Admin.ToString())
            {
                return Forbid();
            }
            await _postService.DeletePostAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/like")]
        public async Task<ActionResult<Post>> LikePost(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user.Role != RoleEnum.Admin.ToString() && user.Role != RoleEnum.User.ToString())
            {
                return Forbid();
            }
            return await _postService.LikePostAsync(id);
        }

        [HttpGet("GetAllPosts")]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            return await _postService.GetAllPostsAsync();
        }
    }
}

