using FluentValidation;
using Restaurants.Application.Customers.Dtos;

namespace Restaurants.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryValidator : AbstractValidator<GetAllCustomersQuery>
    {
        private readonly int[] allowPageSizes = [5, 10, 15, 30];
        private readonly string[] allowedSortByColumnNames = [nameof(CustomerDto.Email),
        nameof(CustomerDto.PhoneNumber),
        nameof(CustomerDto.Name)];


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
