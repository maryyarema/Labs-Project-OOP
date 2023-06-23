using FluentValidation;
using UserManagementEF.UserManagementEF.BLL.DTO;
using UserManagementEF.UserManagementEF.DAL.Entities.Identity;

namespace UserManagementEF.UserManagementEF.API.Validation
{
    public class LoginModel_Validator : AbstractValidator<LoginModel>
    {
        public LoginModel_Validator()
        {
            RuleFor(entity => entity.UserName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("The username must not be empty, and must not exceed 50 characters in length");
            
            RuleFor(entity => entity.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25)
                .WithMessage("The password must not be empty, and must not exceed 25 characters in length");
        }
    }
}