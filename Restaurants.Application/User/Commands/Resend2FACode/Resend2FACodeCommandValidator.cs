using FluentValidation;

namespace Restaurants.Application.User.Commands.Resend2FACode
{
    public class Resend2FACodeCommandValidator : AbstractValidator<Resend2FACodeCommand>
    {
        public Resend2FACodeCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
        }
    }
}
