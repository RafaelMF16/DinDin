using DinDin.Domain.Users;
using DinDin.Services.Users;
using DinDin.Web.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto newUserDto)
        {
            var newUser = new User
            {
                Name = newUserDto.Name,
                Email = newUserDto.Email,
                Password = newUserDto.Password
            };

            await _userService.Add(newUser);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _userService.AuthenticateUser(loginDto.Email, loginDto.Password);
            if (token == null)
                return Unauthorized(new { message = "Incorrect email or password" });

            return Ok(new { Token = token});
        }
    }
}