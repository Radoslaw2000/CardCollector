using CardCollector.DTOs.Auth;
using CardCollector.Services;
using CardCollector.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardCollector.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                var user = _authService.Register(request);
                return Ok(new { message = "User registered successfully", userId = user.UserId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = "xdddddddddd" + ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = _authService.Login(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
