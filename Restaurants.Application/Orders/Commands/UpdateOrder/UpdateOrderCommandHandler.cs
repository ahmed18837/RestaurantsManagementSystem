using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Orders.Commands.UpdateOrder;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Ratings.Commands.UpdateRating
{
    public class UpdateOrderCommandHandler(ILogger<UpdateOrderCommandHandler> logger,
    IOrdersRepository ordersRepository,
    IDishesRepository dishesRepository) : IRequestHandler<UpdateOrderCommand>
    {
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Order {Id}", request.Id);

            var order = await ordersRepository.GetByIdIncludeWithOrderItemsAsync(request.Id)
                       ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

            foreach (var item in request.Items)
            {
                if (item.OrderItemId > 0)
                {
                    var orderItem = order.OrderItems
                                    .FirstOrDefault(i => i.Id == item.OrderItemId)
                               ?? throw new NotFoundException(nameof(OrderItem), item.OrderItemId.ToString());

                    item.Quantity = item.Quantity;
                }
                else
                {
                    var dish = await dishesRepository.GetByIdAsync(item.DishId)
                               ?? throw new NotFoundException(nameof(Dish), item.DishId.ToString());

                    order.OrderItems.Add(new OrderItem
                    {
                        DishId = dish.Id,
                        Quantity = item.Quantity,
                        UnitPrice = dish.Price
                    });
                }
            }

            order.TotalPrice = order.OrderItems.Sum(i => i.UnitPrice * i.Quantity);
            //order.Quantity = order.OrderItems.Sum(i => i.Quantity);

            await ordersRepository.SaveChanges();
        }
    }
}
