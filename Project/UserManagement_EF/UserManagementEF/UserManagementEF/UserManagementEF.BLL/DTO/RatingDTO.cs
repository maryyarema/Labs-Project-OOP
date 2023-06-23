using UserManagementEF.UserManagementEF.BLL.DTO.HATEOAS;

namespace UserManagementEF.UserManagementEF.BLL.DTO
{
    public class RatingDTO : LinkBaseEntity
    {
        public int RatingId { get; set; }
      
        public string? MovieTitle { get; set; } = default!;

        /*  public string? UserFullName { get; set; } = default!;
          public string? BookTitle { get; set; } = default!;*/
    }
}
