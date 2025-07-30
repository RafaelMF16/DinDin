using DinDin.Domain.Constantes;
using DinDin.Domain.Users;
using DinDin.Services.Auth;
using DinDin.Services.Users;
using DinDin.Web.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserService userService, AuthService authService) : ControllerBase
    {
        private readonly UserService _userService = userService;
        private readonly AuthService _authService = authService;

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
            if (loginResponseDto is null)
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

            return Ok(new
            {
                accessToken = loginResponseDto.AccessToken
            });
        }

        [HttpPost("verify-refresh-token")]
        public async Task<IActionResult> VerifyRefreshToken()
        {
            var refreshToken = Request.Cookies[ApplicationConstants.REFRESH_TOKEN_KEY_NAME]
                ?? throw new ArgumentNullException("Não foi possível renovar token");

            var loginResponseDto = await _authService.VerifyRefreshToken(refreshToken);

            if (loginResponseDto is null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = ApplicationConstants.AUTHENTICATION_ERROR_TITLE,
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = ApplicationConstants.SESSION_EXPIRED_MESSAGE,
                    Instance = HttpContext.Request.Path
                };

                return Unauthorized(problemDetails);
            }

            SetRefreshTokenCookie(loginResponseDto.RefreshToken);
            return Ok(new 
            { 
                accessToken = loginResponseDto.AccessToken
            });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies[ApplicationConstants.REFRESH_TOKEN_KEY_NAME]
                ?? throw new ArgumentNullException("Não foi possível renovar token");
            await _authService.LogoutUser(refreshToken);

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