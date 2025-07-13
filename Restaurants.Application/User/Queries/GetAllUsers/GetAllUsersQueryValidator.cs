using FluentValidation;
using Restaurants.Application.User.Dtos;

namespace Restaurants.Application.User.Queries.GetAllUsers
{
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        private readonly int[] allowPageSizes = [5, 10, 15, 30];
        private readonly string[] allowedSortByColumnNames = [nameof(UserDto.Email),nameof(UserDto.Id),nameof(UserDto.Roles),
        nameof(UserDto.PhoneNumber),
        nameof(UserDto.FullName)];

        public GetAllUsersQueryValidator()
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
