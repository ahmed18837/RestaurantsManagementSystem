using FluentValidation;

namespace Restaurants.Application.User.Commands.Send2FACode
{
    public class Send2FACodeCommandValidator : AbstractValidator<Send2FACodeCommand>
    {
        public Send2FACodeCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
        }
    }
}
