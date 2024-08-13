using IS.Domain.Models;
using IS.Domain.Models.Dtos;
using IS.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IS.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserService _userService;

        public ProfileController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetProfile/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPut("EditProfile/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            MapUpdatedProperties(user, userUpdateDto);

            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

        private void MapUpdatedProperties(User user, UserUpdateDto userUpdateDto)
        {
            user.Bio = userUpdateDto.Bio;
            user.Email = userUpdateDto.Email;
            user.UserName = userUpdateDto.UserName;
            user.Name = userUpdateDto.Name;
            user.LastName = userUpdateDto.LastName;
            user.DateOfBirth = userUpdateDto.DateOfBirth;
            user.ProfilePicture = userUpdateDto.ProfilePicture;
            user.Website = userUpdateDto.Website;
            user.Location = userUpdateDto.Location;
        }

    }

}
