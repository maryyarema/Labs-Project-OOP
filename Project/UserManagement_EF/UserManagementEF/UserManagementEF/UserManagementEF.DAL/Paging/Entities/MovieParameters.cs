namespace UserManagementEF.UserManagementEF.DAL.Paging.Entities
{
    public class MovieParameters : BaseParameters
    {
        public MovieParameters()
        {
            OrderBy = " title, release_year desc"; // default sorting
        }
    }
}
