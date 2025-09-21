using System.ComponentModel.DataAnnotations;

namespace CardCollector.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required]
        public string? UsernameOrEmail { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
