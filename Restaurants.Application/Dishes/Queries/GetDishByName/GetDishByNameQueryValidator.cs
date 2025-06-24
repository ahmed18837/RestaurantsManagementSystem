using FluentValidation;

namespace Restaurants.Application.Dishes.Queries.GetDishByName
{
    public class GetDishByNameQueryValidator : AbstractValidator<GetDishByNameQuery>
    {
        public GetDishByNameQueryValidator()
        {
            RuleFor(q => q.RestaurantId)
            .GreaterThan(0)
            .WithMessage("RestaurantId must be greater than zero");

            RuleFor(q => q.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Max length of Name is 100 characters");
        }
    }
}
