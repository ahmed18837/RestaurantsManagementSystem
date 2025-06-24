using FluentValidation;

namespace Restaurants.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(c => c.Name)
              .NotEmpty().WithMessage("Name is required")
              .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid email address");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^(\+201[0125][0-9]{8}|01[0125][0-9]{8})$").WithMessage("Phone number must be a valid Egyptian mobile number");
        }
    }
}
