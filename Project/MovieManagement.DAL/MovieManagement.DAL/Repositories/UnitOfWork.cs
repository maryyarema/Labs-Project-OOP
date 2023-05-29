using MovieManagement.DAL.Repositories.Contracts;
using System.Data;

namespace MovieManagement.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public  IMovieRepository _movieRepository { get; }
        public IActorRepository  _actorRepository { get; }
        public ICategoryRepository _categoryRepository { get; }
        public IMovieActorRepository _movieActorRepository { get; }
        public IMovieCategoryRepository _movieCategoryRepository { get; }

        readonly IDbTransaction _dbTransaction;

        public UnitOfWork(
            

         IMovieRepository movieRepository,
        IActorRepository actorRepository,
        ICategoryRepository  categoryRepository ,
        IMovieActorRepository movieActorRepository ,
         IMovieCategoryRepository movieCategoryRepository ,
           
            IDbTransaction dbTransaction)
        {


            this._movieRepository = movieRepository;
            this._actorRepository = actorRepository;
            this._categoryRepository = categoryRepository;
            this._movieActorRepository = movieActorRepository;
            this._movieCategoryRepository = movieCategoryRepository;
                       
            _dbTransaction = dbTransaction;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                // By adding this we can have muliple transactions as part of a single request
                //_dbTransaction.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
                Console.WriteLine(ex.Message);
            }
        }
        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}