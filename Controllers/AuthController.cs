using BCrypt.Net;
using CardCollector.Data;
using CardCollector.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
                return BadRequest("Username already exists");

            var user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Registered");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null)
                return BadRequest("User does not exist");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user!.Password))
                return Unauthorized("Wrong username or password");

            _context.SaveChanges();
            // TODO: jwt token
            return Ok(new {userId = user.UserId, accesToken = Guid.NewGuid() });
        }

    }
}
