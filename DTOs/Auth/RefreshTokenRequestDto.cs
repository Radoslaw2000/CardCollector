using System.ComponentModel.DataAnnotations;

namespace CardCollector.DTOs.Auth
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
