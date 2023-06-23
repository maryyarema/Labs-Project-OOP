using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using UserManagementEF.DAL.Entities.Identity;

namespace UserManagementEF.DAL.Entities
{
    [Table("Users")]
    public class User : IdentityUser<int>
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; } = default!;

        [Required]
        [Column("username")]
        [MaxLength(255)]
        public string Username { get; set; } = default!;

        [Required]
        [Column("password")]
        [MaxLength(255)]
        public string Password { get; set; } = default!;

        [Required]
        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; } = default!;

        [Required]
        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; } = default!;

        [Required]
        public List<RefreshToken>? RefreshTokens { get; set; }

        public ICollection<Comment> Comments { get; set; } = default!; // one-to-many
        public ICollection<Rating> Ratings { get; set; } = default!;// one-to-many
    }
}

