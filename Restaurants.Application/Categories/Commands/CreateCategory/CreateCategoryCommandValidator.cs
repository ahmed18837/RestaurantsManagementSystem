using FluentValidation;

namespace Restaurants.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .MaximumLength(100)
                .WithMessage("Max Length Of Name is 100 Characters");


            RuleFor(dto => dto.Description)
                .MaximumLength(500)
                .WithMessage("Max Length Of Comment is 500 Characters");
        }
    }
}
