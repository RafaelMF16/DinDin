using System.Security.Claims;
using DinDin.Domain.Constantes;
using DinDin.Domain.Extensions;
using DinDin.Domain.Users;
using DinDin.Services.Users;
using DinDin.Web.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

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
            var loginResponseDto = await _userService.AuthenticateUser(loginDto.Email, loginDto.Password);
            if (loginResponseDto == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = ApplicationConstants.AUTHENTICATION_ERROR_TITLE,
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = ApplicationConstants.AUTHENTICATION_ERROR_MESSAGE,
                    Instance = HttpContext.Request.Path
                };
                return Unauthorized(problemDetails);
            }

            SetRefreshTokenCookie(loginResponseDto.RefreshToken);

            return Ok(loginResponseDto);
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.GetUserId();
            await _userService.LogoutUser(userId);
            ClearRefreshTokenCookie();
            return Ok();
        }

        private void ClearRefreshTokenCookie()
        {
            Response.Cookies.Delete(ApplicationConstants.REFRESH_TOKEN_KEY_NAME);
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(ApplicationConstants.REFRESH_TOKEN_EXPIRATION_DAYS),
                Path = ApplicationConstants.REFRESH_TOKEN_CONTROLLER_PATH
            };

            Response.Cookies.Append(ApplicationConstants.REFRESH_TOKEN_KEY_NAME, refreshToken, cookieOption);
        }
    }
}