using FluentValidation;

namespace Restaurants.Application.Customers.Queries.GetCustomerByEmail
{
    public class GetCustomerByEmailQueryValidator : AbstractValidator<GetCustomerByEmailQuery>
    {
        public GetCustomerByEmailQueryValidator()
        {
            RuleFor(c => c.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Email must be a valid email address");
        }
    }
}
