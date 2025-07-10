using FluentValidation;

namespace Restaurants.Application.User.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.ResetCode)
                .NotEmpty().WithMessage("Reset Code is required.")
                .Length(6).WithMessage("Reset Code must be 6 characters.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(64).WithMessage("Password cannot exceed 64 characters.");
        }
    }
}
