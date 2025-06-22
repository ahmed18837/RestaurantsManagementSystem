using FluentValidation;

namespace Restaurants.Application.Ratings.Commands.UpdateRating
{
    public class UpdateRatingCommandValidator : AbstractValidator<UpdateRatingCommand>
    {
        public UpdateRatingCommandValidator()
        {
            RuleFor(dto => dto.Stars)
             .InclusiveBetween(1, 5)
             .WithMessage("Stars Must Be 1 : 5");

        }
    }
}
