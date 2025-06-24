using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantByName
{
    public class GetRestaurantByNameQueryValidator : AbstractValidator<GetRestaurantByNameQuery>
    {
        public GetRestaurantByNameQueryValidator()
        {
            RuleFor(dto => dto.Name)
                .MaximumLength(100)
                .WithMessage("Max Length Of Name is 100 Characters");
        }
    }
}
