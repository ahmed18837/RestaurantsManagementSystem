using FluentValidation;

namespace Restaurants.Application.Categories.Queries.GetCategoryByName
{
    public class GetCategoryByNameQueryValidator : AbstractValidator<GetCategoryByNameQuery>
    {
        public GetCategoryByNameQueryValidator()
        {
            RuleFor(dto => dto.Name)
                .MaximumLength(100)
                .WithMessage("Max Length Of Name is 100 Characters");
        }
    }
}
