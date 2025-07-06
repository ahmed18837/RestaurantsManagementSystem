using FluentValidation;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishByCategoryIdForRestaurant
{
    public class GetDishesByCategoryIdForRestaurantQueryValidator : AbstractValidator<GetDishesByCategoryIdForRestaurantQuery>
    {
        private readonly int[] allowPageSizes = [5, 10, 15, 30];
        private readonly string[] allowedSortByColumnNames = [nameof(DishDto.Name),
        nameof(DishDto.Price),
        nameof(DishDto.Description)];


        public GetDishesByCategoryIdForRestaurantQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => allowPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

            RuleFor(r => r.SortBy)
                .Must(value => allowedSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
