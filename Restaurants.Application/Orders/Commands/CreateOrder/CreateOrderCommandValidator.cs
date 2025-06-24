using FluentValidation;

namespace Restaurants.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.CustomerId)
                 .GreaterThan(0).WithMessage("CustomerId must be positive");

            RuleFor(c => c.Items)
                .NotEmpty().WithMessage("Order must contain at least one item");

            RuleForEach(c => c.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.DishId)
                     .GreaterThan(0).WithMessage("DishId must be positive");

                items.RuleFor(i => i.Quantity)
                     .GreaterThan(0).WithMessage("Quantity must be at least 1");
            });
        }
    }
}
