using FluentValidation;
using TodoProject.EntityLayer.Concrete;

namespace TodoProject.WebApi.Validators
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            // nullar otomatik yakalanıyor zaten

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title area can not be empty")
                .Length(1,30);

            RuleFor(x => x.Content).NotEmpty().WithMessage("Content area can not be empty")
                .Length(1, 500);
        }
    }
}
