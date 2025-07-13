using FluentValidation;

namespace Restaurants.Application.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(c => c.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Email must be a valid email address");
        }
    }
}
