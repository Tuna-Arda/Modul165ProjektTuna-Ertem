using JetstreamBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace JetstreamBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MariaDbController : ControllerBase
    {
        private readonly MariaDbService _mariaDbService;
        private readonly UserService _userService;
        
        public MariaDbController(MariaDbService mariaDbService, UserService userService)
        {
            _mariaDbService = mariaDbService;
            _userService = userService;
        }
        
        private bool ValidateToken(out string token)
        {
            token = Request.Headers["x-access-token"];
            if (string.IsNullOrEmpty(token)) return false;
            var user = _userService.GetByUsername(token);
            return user != null;
        }
        
        [HttpGet("orders")]
        public IActionResult GetOrders()
        {
            if (!ValidateToken(out _))
                return Unauthorized(new { message = "Token fehlt oder ungültig" });
            var orders = _mariaDbService.GetOrders();
            return Ok(orders);
        }
    }
}
