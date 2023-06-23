using Microsoft.Build.Framework;

namespace UserManagementEF.UserManagementEF.DAL.Entities.Identity
{
    public class RevokeTokenRequest
    {
        [Required]
        public string? Token { get; set; }
    }
}