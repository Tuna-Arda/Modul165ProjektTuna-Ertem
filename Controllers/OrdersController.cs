using JetstreamBackend.Models;
using JetstreamBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace JetstreamBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly UserService _userService;
        
        public OrdersController(OrderService orderService, UserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        
        private bool ValidateToken(out string token)
        {
            token = Request.Headers["x-access-token"];
            if (string.IsNullOrEmpty(token)) return false;
            var user = _userService.GetByUsername(token);
            return user != null;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            if (!ValidateToken(out _))
                return Unauthorized(new { message = "Token fehlt oder ungültig" });
            return Ok(_orderService.Get());
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            if (!ValidateToken(out _))
                return Unauthorized(new { message = "Token fehlt oder ungültig" });
            _orderService.Create(order);
            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }
        
        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public IActionResult Get(string id)
        {
            if (!ValidateToken(out _))
                return Unauthorized(new { message = "Token fehlt oder ungültig" });
            var order = _orderService.Get(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }
        
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Order orderIn)
        {
            if (!ValidateToken(out _))
                return Unauthorized(new { message = "Token fehlt oder ungültig" });
            var order = _orderService.Get(id);
            if (order == null)
                return NotFound();
            orderIn.Id = order.Id;
            _orderService.Update(id, orderIn);
            return NoContent();
        }
        
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (!ValidateToken(out _))
                return Unauthorized(new { message = "Token fehlt oder ungültig" });
            var order = _orderService.Get(id);
            if (order == null)
                return NotFound();
            _orderService.Remove(id);
            return NoContent();
        }
    }
}
