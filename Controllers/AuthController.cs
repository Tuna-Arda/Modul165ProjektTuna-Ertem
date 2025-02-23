using JetstreamBackend.Models;
using JetstreamBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace JetstreamBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        
        public AuthController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _userService.GetByUsername(login.Username);
            if (user == null || user.Password != login.Password)
                return Unauthorized(new { message = "Ungültige Anmeldedaten" });
            
            // Für diese Demo entspricht der Token dem Benutzernamen
            return Ok(new { token = user.Username, role = user.Role });
        }
    }
}
