using FluentValidation;
using Restaurants.Application.Orders.Dtos;

namespace Restaurants.Application.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryValidator : AbstractValidator<GetOrdersByCustomerIdQuery>
    {
        private readonly int[] AllowPageSizes = [5, 10, 15, 30];
        private readonly string[] AllowedSortByColumnNames = [nameof(OrderDto.TotalPrice),
        nameof(OrderDto.OrderDate),nameof(OrderDto.CustomerName)];


        public GetOrdersByCustomerIdQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => AllowPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", AllowPageSizes)}]");

            RuleFor(r => r.SortBy)
                .Must(value => AllowedSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", AllowedSortByColumnNames)}]");
        }
    }
}
