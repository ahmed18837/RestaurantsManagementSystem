using FluentValidation;
using Restaurants.Application.Ratings.Dtos;

namespace Restaurants.Application.Ratings.Queries.GetAllRatings
{
    public class GetAllRatingsQueryValidator : AbstractValidator<GetAllRatingsQuery>
    {
        private int[] allowPageSizes = [5, 10, 15, 30];
        private string[] allowedSortByColumnNames = [nameof(RatingDto.Comment),
        nameof(RatingDto.Stars),
        nameof(RatingDto.CreatedAt)];


        public GetAllRatingsQueryValidator()
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
