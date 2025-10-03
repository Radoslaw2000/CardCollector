using System.ComponentModel.DataAnnotations;

namespace CardCollector.DTOs.Auth
{
    public class LogoutDto
    {
        [Required]
        public Guid RefreshToken { get; set; }
    }
}
