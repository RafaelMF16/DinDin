using DinDin.Domain.Users;
using DinDin.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]User user)
        {
            _userService.Add(user);
            return Ok(user);
        }
    }
}