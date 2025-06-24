using FluentValidation;

namespace Restaurants.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(cmd => cmd.Id)
                .GreaterThan(0)
                .WithMessage("Order Id must be greater than 0.");

            RuleFor(cmd => cmd.Items)
                .NotNull().WithMessage("Order must contain at least one item.")
                .Must(items => items.Any()).WithMessage("Order must contain at least one item.");

            RuleForEach(cmd => cmd.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.OrderItemId)
                    .GreaterThan(0).WithMessage("OrderItemId must be greater than 0.");

                item.RuleFor(i => i.DishId)
                    .GreaterThan(0).WithMessage("DishId must be greater than 0.");

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            });
        }
    }
}
