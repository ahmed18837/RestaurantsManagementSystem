using FluentValidation;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.User.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(50).WithMessage("Full Name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.UserType)
                .NotEmpty().WithMessage("User type is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(64).WithMessage("Password cannot exceed 64 characters.");

            RuleFor(x => x.UserType)
                .Must(type => new[] { UserRoles.User, UserRoles.Admin, UserRoles.Owner }.Contains(type))
                .WithMessage("UserType must be one of: Admin, User, Owner.");

        }
    }
}
