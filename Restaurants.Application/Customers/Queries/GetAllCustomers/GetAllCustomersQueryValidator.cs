using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryValidator : AbstractValidator<GetAllCustomersQuery>
    {
        private int[] allowPageSizes = [5, 10, 15, 30];
        private string[] allowedSortByColumnNames = [nameof(RestaurantDto.Name),
        nameof(RestaurantDto.Category),
        nameof(RestaurantDto.Description)];


        public GetAllCustomersQueryValidator()
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
