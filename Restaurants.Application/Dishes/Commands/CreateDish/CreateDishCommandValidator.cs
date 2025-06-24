using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .MaximumLength(100)
                .WithMessage("Max Length Of Name is 100 Characters");


            RuleFor(dto => dto.Description)
                .MaximumLength(500)
                .WithMessage("Max Length Of Comment is 500 Characters");

            RuleFor(dto => dto.Price)
                .GreaterThan(0)
                .LessThan(1000)
                .WithMessage("Price Must be Greater Than Zero");

            //RuleFor(x => x.ImageFileName)
            //.Matches(@"^.*\.(jpg|jpeg|png|gif)$")
            //.When(x => !string.IsNullOrWhiteSpace(x.ImageFileName));

        }
    }
}
