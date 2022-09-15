using API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    /// <summary>
    /// Application user
    /// </summary>
    public sealed class AppUser
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string? UserName { get;set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(20)]
        public string? KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LastActive { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string? Gender { get; set; }

        [StringLength(200)]
        public string? Introduction { get; set; }

        [StringLength(200)]
        public string? LookingFor { get; set; }
        
        [StringLength(200)]
        public string? Interests { get; set; }

        [StringLength(30)]
        public string? City { get; set; }

        [StringLength(30)]
        public string? Country { get; set; }

        public ICollection<Photo>? Photos { get; set; }

        public ICollection<UserLike>? LikedByUsers { get; set; }

        public ICollection<UserLike>? LikedUsers { get; set; }

        public ICollection<Message>? MessagesSent { get; set; }

        public ICollection<Message>? MessagesReceived { get; set; }
    }
}
