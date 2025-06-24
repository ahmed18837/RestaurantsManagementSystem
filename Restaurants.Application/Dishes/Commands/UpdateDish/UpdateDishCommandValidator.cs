using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommandValidator : AbstractValidator<UpdateDishCommand>
    {
        public UpdateDishCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .MaximumLength(100)
                .When(dto => dto.Name is not null)
                .WithMessage("Max length of Name is 100 characters");

            RuleFor(dto => dto.Description)
                .MaximumLength(500)
                .When(dto => dto.Description is not null)
                .WithMessage("Max length of Description is 500 characters");
        }
    }
}
