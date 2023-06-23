using FluentValidation;
using UserManagementEF.UserManagementEF.DAL.Entities.Identity;

namespace UserManagementEF.UserManagementEF.API.Validation
{
    public class AuthenticationModel_Validator : AbstractValidator<AuthenticationModel>
    {
        public AuthenticationModel_Validator()
        {
            RuleFor(entity => entity.Message)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Message must not be empty");
            
            RuleFor(entity => entity.UserName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("The UserName must not be empty, and must not exceed 50 characters in length");
            
            RuleFor(entity => entity.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("The email entered must meet the email standard");
            
            RuleFor(entity => entity.Token)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Token must not be empty");
            
            RuleFor(entity => entity.Roles)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Roles must not be empty");
        }
    }
}