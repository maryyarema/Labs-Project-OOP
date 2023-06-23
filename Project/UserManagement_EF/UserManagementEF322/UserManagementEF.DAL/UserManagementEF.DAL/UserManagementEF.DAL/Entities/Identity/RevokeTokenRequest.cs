using Microsoft.Build.Framework;

namespace SchoolLibrary_EF.DAL.Entities.Identity
{
    public class RevokeTokenRequest
    {
        [Required]
        public string? Token { get; set; }
    }
}