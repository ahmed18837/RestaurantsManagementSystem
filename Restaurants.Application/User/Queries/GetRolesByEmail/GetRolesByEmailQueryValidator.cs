using FluentValidation;

namespace Restaurants.Application.User.Queries.GetRolesByEmail
{
    public class GetRolesByEmailQueryValidator : AbstractValidator<GetRolesByEmailQuery>
    {
        public GetRolesByEmailQueryValidator()
        {
            RuleFor(c => c.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Email must be a valid email address");
        }
    }
}
