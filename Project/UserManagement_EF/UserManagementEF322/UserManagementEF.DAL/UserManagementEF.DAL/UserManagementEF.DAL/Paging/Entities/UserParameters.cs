namespace UserManagementEF.DAL.Paging.Entities
{
    public class UserParameters : BaseParameters
    {
        public string? Username { get; set; } = default!;

        public UserParameters()
        {
            OrderBy = "user_id"; // default sorting
        }
    }
}
