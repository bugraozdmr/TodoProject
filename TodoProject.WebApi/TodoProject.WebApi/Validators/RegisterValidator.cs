using FluentValidation;
using TodoProject.DtoLayer.RegisterDto;

namespace TodoProject.WebApi.Validators
{
    public class RegisterValidator : AbstractValidator<CreateNewUserDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("give your username");
            RuleFor(x => x.Password).NotEmpty().WithMessage("give your password");
        }

    }
}
