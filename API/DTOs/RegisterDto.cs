using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    /// <summary>
    /// Represents register entity of the user
    /// </summary>
    public class RegisterDto
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
