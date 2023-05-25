using MovieManagement.DAL.Repositories.Contracts;

namespace MovieManagement.DAL.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository _movieRepository { get; }
        void Commit();
        void Dispose();
    }
}
