﻿using System.ComponentModel.DataAnnotations;

namespace UserManagementEF.UserManagementEF.DAL.Entities.Identity
{
    public class AddRoleModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}