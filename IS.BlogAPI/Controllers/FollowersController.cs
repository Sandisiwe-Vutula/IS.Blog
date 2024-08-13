using IS.Domain.Models;
using IS.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IFollowersService _followersService;

        public FollowersController(IFollowersService followersService)
        {
            _followersService = followersService;
        }

        [HttpGet("GetFollower/{id}")]
        public async Task<ActionResult<Followers>> GetFollower(int id)
        {
            return await _followersService.GetFollowerAsync(id);
        }

        [HttpPut("UpdateFollower")]
        public async Task<ActionResult> UpdateFollower(Followers follower)
        {
            await _followersService.UpdateFollowerAsync(follower);
            return NoContent();
        }
    }
}
