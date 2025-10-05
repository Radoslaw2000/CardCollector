using System.ComponentModel.DataAnnotations;

namespace CardCollector.DTOs.Auth
{
    public class LogoutDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
