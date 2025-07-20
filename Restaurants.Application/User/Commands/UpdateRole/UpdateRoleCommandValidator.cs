using FluentValidation;

namespace Restaurants.Application.User.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.OldRoleName)
                .NotEmpty().WithMessage("User type is required.")
                .MaximumLength(20).WithMessage("Email cannot exceed 20 characters.");

            RuleFor(x => x.NewRoleName)
                .NotEmpty().WithMessage("User type is required.")
                .MaximumLength(20).WithMessage("Email cannot exceed 20 characters.");
        }
    }
}
