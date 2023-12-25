using FluentValidation;
using TodoProject.DtoLayer.LoginDto;

namespace TodoProject.WebApi.Validators
{
    public class LoginValidator : AbstractValidator<LoginUserDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("give your username");
            RuleFor(x => x.Password).NotEmpty().WithMessage("give your password");
        }
    }
}
