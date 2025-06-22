using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

        public UpdateRestaurantCommandValidator()
        {
            RuleFor(c => c.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Category)
              .NotEmpty()
              .WithMessage("Invalid category. Please choose from the valid categories.");

            RuleFor(dto => dto.Category)
                .Must(validCategories.Contains)
                .WithMessage("Category Invalid category. Please choose from the valid categories.");

            RuleFor(dto => dto.Category)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var isValidCategory = validCategories.Contains(value);
                    if (!isValidCategory)
                    {
                        context.AddFailure("Category", "Invalid category. Please choose from the valid categories.");
                    }
                });

            RuleFor(c => c.Category)
                .Length(3, 100);

            RuleFor(dto => dto.Description)
            .Length(5, 500);
        }
    }
}
