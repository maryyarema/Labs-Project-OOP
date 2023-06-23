using FluentValidation;
using UserManagementEF.UserManagementEF.BLL.DTO;

namespace UserManagementEF.UserManagementEF.API.Validation
{
    public class UserDTO_Validator : AbstractValidator<UserDTO>
    {
        public UserDTO_Validator()
        {
            RuleFor(entity => entity.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("The email entered must meet the email standard");
        }
    }
}