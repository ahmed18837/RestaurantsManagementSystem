using FluentValidation;

namespace Restaurants.Application.Customers.Queries.GetCustomerByName
{
    public class GetCustomerByNameQueryValidator : AbstractValidator<GetCustomerByNameQuery>
    {
        public GetCustomerByNameQueryValidator()
        {
            RuleFor(dto => dto.Name)
                .MaximumLength(100)
                .WithMessage("Max Length Of Name is 100 Characters");
        }
    }
}
