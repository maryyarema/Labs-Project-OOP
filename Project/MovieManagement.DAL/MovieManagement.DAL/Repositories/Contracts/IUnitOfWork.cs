using MovieManagement.DAL.Repositories.Contracts;

namespace MovieManagement.DAL.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository _movieRepository { get; }
        ICategoryRepository _categoryRepository { get; }
        IActorRepository _actorRepository { get; }
        IMovieCategoryRepository _movieCategoryRepository { get; }
        IMovieActorRepository _movieActorRepository { get; }
        void Commit();
      
    }
}
