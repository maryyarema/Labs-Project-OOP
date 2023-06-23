using UserManagementEF.UserManagementEF.BLL.DTO.HATEOAS;

namespace UserManagementEF.UserManagementEF.BLL.DTO
{
    public class UserDTO : LinkBaseEntity
    {
        public int UserId { get; set; }
        public DateTime? RegistrationDate { get; set; } = default!;
        public string? Email { get; set; } = default!;
        //public string Password { get; set; } = default!;
        //public string Address { get; set; } = default!;
        //public string Phone { get; set; } = default!;
    }
}
