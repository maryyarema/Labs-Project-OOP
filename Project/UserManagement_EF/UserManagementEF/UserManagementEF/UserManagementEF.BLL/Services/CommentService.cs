using System.Dynamic;
using UserManagementEF.UserManagementEF.API.Mapping.Configurations;
using UserManagementEF.UserManagementEF.BLL.DTO;
using UserManagementEF.UserManagementEF.BLL.Services.Contracts;
using UserManagementEF.UserManagementEF.DAL.Entities;
using UserManagementEF.UserManagementEF.DAL.Paging;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;
using UserManagementEF.UserManagementEF.DAL.Repositories.Contracts;

namespace UserManagementEF.UserManagementEF.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;

        public CommentService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<int> CreateAsync(CommentDTO entity)
        {
            // We create a Loan object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Comment comment = MappingFunctions.MapSourceToDestination<CommentDTO, Comment>(entity);


            await SeedingCommentObject(entity, comment);

            var id = await _uow.Comments.CreateAsync(comment);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<CommentDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Comment, CommentDTO>
                (await _uow.Comments.GetAllAsync(parameters));
        }
        public async Task<CommentDTO?> GetAsync(int id)
        {
            // Get entity from db
            Comment? comment = await _uow.Comments.GetByIdAsync(id);

            // We create a LoanDTO object and copy the values ​​of the properties
            // of the comment object into its properties (we perform mapping)
            CommentDTO? commentDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                comment == null ?
                null : MappingFunctions.MapSourceToDestination<Comment, CommentDTO>(comment);

            return commentDTO;
        }
        public async Task UpdateAsync(CommentDTO entity)
        {
            // We create a Loan object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Comment comment = MappingFunctions.MapSourceToDestination<CommentDTO, Comment>(entity);

            await SeedingCommentObject(entity, comment);

            await _uow.Comments.UpdateAsync(comment);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow.Comments.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<DurationList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.Comments.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null)
        {
            return await _uow.Comments.GetById_DataShaping_Async(id, parameters);
        }

        private async Task SeedingCommentObject(CommentDTO entity, Comment comment)
        {
            var movie = (await _uow.Movies.GetAllAsync()).ToList()
                .FirstOrDefault(b => b.Title == entity.MovieTitle);
            

            if (movie == null) throw new Exception("No movie with this title was found");
          
         
            comment.Movie = movie;
        }
    }
}
