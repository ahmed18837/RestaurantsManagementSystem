using FluentValidation;

namespace Restaurants.Application.Ratings.Commands.CreateRating
{
    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(dto => dto.Stars)
      .InclusiveBetween(1, 5)
      .WithMessage("Stars Must Be 1 : 5");


            RuleFor(dto => dto.Comment)
                .MaximumLength(500)
                .WithMessage("Max Length Of Comment is 500 Characters");
        }
    }
}
