using FluentValidation;

namespace Restaurants.Application.Customers.Queries.GetCustomerByPhoneNumber
{
    public class GetCustomerByPhoneNumberQueryValidator : AbstractValidator<GetCustomerByPhoneNumberQuery>
    {
        public GetCustomerByPhoneNumberQueryValidator()
        {
            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^(\+201[0125][0-9]{8}|01[0125][0-9]{8})$").WithMessage("Phone number must be a valid Egyptian mobile number");
        }
    }
}
