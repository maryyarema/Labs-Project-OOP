using UserManagementEF.UserManagementEF.BLL.DTO.HATEOAS;

namespace UserManagementEF.UserManagementEF.BLL.DTO
{
    public class MovieDTO : LinkBaseEntity
    {

        public int MovieId { get; set; }
        public string? Title { get; set; } = default!;
        public string? MovieTitle { get; set; } = default!;
        /*  public int ReleaseYear { get; set; }
         public int MovieId { get; set; }
          public string? Director { get; set; } = default!;
          public string? Description { get; set; } = default!;
          public int Duration { get; set; }
      }*/
    }
}
