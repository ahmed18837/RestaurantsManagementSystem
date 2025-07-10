using FluentValidation;

namespace Restaurants.Application.User.Commands.Verify2FACode
{
    internal class Verify2FACodeCommandValidator : AbstractValidator<Verify2FACodeCommand>
    {
        public Verify2FACodeCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Reset Code is required.")
                .Length(6).WithMessage("Reset Code must be 6 characters.");
        }
    }
}
