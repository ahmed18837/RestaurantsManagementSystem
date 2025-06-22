using FluentValidation;

namespace Restaurants.Application.Dishes.Queries.GetDishByName
{
    public class GetDishByNameQueryValidator : AbstractValidator<GetDishByNameQuery>
    {
        public GetDishByNameQueryValidator()
        {
            RuleFor(dto => dto.Name)
                .MaximumLength(100)
                .WithMessage("Max Length Of Name is 100 Characters");
        }
    }
}
