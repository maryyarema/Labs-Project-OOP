using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace UserManagementEF.DAL.Entities.Identity
{
    [Owned]
    public class RefreshToken
    {
        [Required]
        public string? Token { get; set; }
        public DateTime Expires { get; set; }
        private bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}