using FluentValidation;

namespace Restaurants.Application.User.Commands.DeleteRole
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("User type is required.");
        }
    }
}
