namespace UserManagementEF.UserManagementEF.DAL.Paging.Entities
{
    public class CommentParameters : BaseParameters
    {
        public CommentParameters()
        {
            OrderBy = "comment_id"; // default sorting
        }
    }
}
