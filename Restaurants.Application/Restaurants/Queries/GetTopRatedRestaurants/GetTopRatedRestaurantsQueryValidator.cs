using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetTopRatedRestaurants
{
    public class GetTopRatedRestaurantsQueryValidator : AbstractValidator<GetTopRatedRestaurantsQuery>
    {
        public GetTopRatedRestaurantsQueryValidator()
        {
            RuleFor(r => r.Count)
               .GreaterThanOrEqualTo(1)
               .When(x => x != null);
        }
    }
}
