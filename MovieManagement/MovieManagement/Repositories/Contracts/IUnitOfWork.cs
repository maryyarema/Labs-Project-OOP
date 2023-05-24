using System;

namespace MovieManagement.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
          IMovieRepository _movieRepository { get; }
        void Commit();
        void Dispose();
    }
}
