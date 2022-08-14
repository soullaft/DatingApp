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
        public string KnownAs { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
