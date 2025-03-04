using DinDin.Domain.Users;
using DinDin.Services.Users;
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

        [HttpPost]
        public IActionResult Register([FromBody] User newUser)
        {
            _userService.Add(newUser);
            return Created();
        }
    }
}