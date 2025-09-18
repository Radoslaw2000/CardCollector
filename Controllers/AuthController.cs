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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Registered");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (request == null)
                return BadRequest("User does not exist");
            if (!BCrypt.Net.BCrypt.Verify(request.PasswordHash, user!.PasswordHash))
                return Unauthorized("Wrong username or password");

            user.AccessToken = Guid.NewGuid();
            _context.SaveChanges();

            return Ok(new {userId = user.UserId, accesToken = user.AccessToken});
        }

    }
}
