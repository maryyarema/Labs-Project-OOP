using Microsoft.AspNetCore.Identity;
using UserManagementEF.DAL.Data;
using UserManagementEF.DAL.Entities;
using UserManagementEF.DAL.Repository.Contracts;

namespace UserManagementEF.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMovieRepository Movies { get; }
        public IRaitingRepository Raitings { get; }
        public ICommentRepository Comments { get; }
        public IUserRepository Users { get; }


        public UserManager<User> _userManager { get; set; }
        public RoleManager<IdentityRole<int>> _roleManager { get; set; }

        public UnitOfWork(
            UserManagementContext dbContext,


        IMovieRepository movies,
        IRaitingRepository raitings,
        ICommentRepository comments,
        IUserRepository users,

        IBookRepository books,
            IBookDetailsRepository bookDetails,
            IAuthorRepository authors,
            IPublisherRepository publishers,
            IUserRepository users,
            ILoanRepository loans,
            IReviewRepository reviews,
            IGenreRepository genres,
            IBookGenresRepository bookGenres,
            IBookAuthorsRepository bookAuthors,

            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager
            )
        {
            this.dbContext = dbContext;


            Movies = movies;
            Raitings = raitings;
            Comments = comments;
            Users = users;

            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}