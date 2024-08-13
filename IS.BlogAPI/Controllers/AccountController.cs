using IS.Domain.Models;
using IS.Domain.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IS.BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        #region Endpoints
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userExist = await _userManager.FindByEmailAsync(user.EmailAddress);
                    if (userExist != null)
                    {
                        return BadRequest(new AuthenticationResult()
                        {
                            Result = false,
                            Errors = new List<string>() { "Email already exists." }
                        });
                    }

                    var username = $"user{Guid.NewGuid().ToString("N")}";
                    var createUser = new User()
                    {
                        Email = user.EmailAddress,
                        UserName = username,
                        Password = user.Password,
                    };

                    var hashedPassword = _userManager.PasswordHasher.HashPassword(createUser, user.Password);
                    createUser.PasswordHash = hashedPassword;

                    var isUserCreated = await _userManager.CreateAsync(createUser);
                    if (isUserCreated.Succeeded)
                    {
                        var token = GenerateJwtToken(createUser);
                        return Ok(new AuthenticationResult()
                        {
                            Result = true,
                            Token = token
                        });
                    }

                    return BadRequest(new AuthenticationResult()
                    {
                        Errors = new List<string>() { "An error occurred while performing your request, contact admin" },
                        Result = false
                    });
                }
            }
            catch (Exception x)
            {
                throw;
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {

            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(user.EmailAddress);
                if (userExist == null)
                {
                    return BadRequest(new AuthenticationResult()
                    {
                        Errors = new List<string>()
                        {
                            "Account doesn't exist, please register to SignIn"
                        },
                        Result = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(userExist, user.Password);
                if (!isCorrect)
                {
                    return BadRequest(new AuthenticationResult()
                    {
                        Errors = new List<string>()
                        {
                            "Incorrect username or password"
                        },
                        Result = false
                    });
                }

                var jwtToken = GenerateJwtToken(userExist);

                return Ok(new AuthenticationResult()
                {
                    Token = jwtToken,
                    Result = true
                });

            }
            return BadRequest(new AuthenticationResult()
            {
                Errors = new List<string>()
                {
                    "An error occured while perfoming your request, contact admin."
                },
                Result = false
            });
        }
        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            string id = HttpContext.User.FindFirstValue("id");

            if (!Guid.TryParse(id, out Guid userId))
            {
                return Unauthorized();
            }

           // await _refreshTokenService.DeleteAllTokens(userId);

            return NoContent();

        }
        #endregion

        #region Private methods
        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);

            //Token Descriptor to fill all token info
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        #endregion
    }

}
