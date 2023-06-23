using UserManagementEF.UserManagementEF.BLL.DTO.HATEOAS;

namespace UserManagementEF.UserManagementEF.BLL.DTO
{
    public class CommentDTO : LinkBaseEntity
    {
        public int CommentId { get; set; }
                 public string? MovieTitle { get; set; } = default!;
     /*   public string?  { get; set; } = default!;*/

    }
}
