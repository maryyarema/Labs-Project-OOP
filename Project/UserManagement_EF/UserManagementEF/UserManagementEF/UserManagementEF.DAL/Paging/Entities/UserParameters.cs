namespace UserManagementEF.UserManagementEF.DAL.Paging.Entities
{
    public class UserParameters : BaseParameters
    {
        public string? UserName { get; set; } = default!;

        public UserParameters()
        {
            OrderBy = "user_id"; // default sorting
        }
    }
}
