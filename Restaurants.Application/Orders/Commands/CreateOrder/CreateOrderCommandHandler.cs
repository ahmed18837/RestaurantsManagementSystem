using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger,
        IOrdersRepository ordersRepository, IDishesRepository dishesRepository,
        ICustomersRepository customersRepository) : IRequestHandler<CreateOrderCommand, int>
    {
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _ = await customersRepository.GetByIdAsync(request.CustomerId)
                ?? throw new NotFoundException(nameof(Customer), request.CustomerId.ToString());

            var order = new Order
            {
                CustomerId = request.CustomerId
            };

            decimal totalPrice = 0;

            foreach (var itemDto in request.Items)
            {
                var dish = await dishesRepository.GetByIdAsync(itemDto.DishId)
                           ?? throw new NotFoundException(nameof(Dish), itemDto.DishId.ToString());

                var orderItem = new OrderItem
                {
                    DishId = dish.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = dish.Price
                };

                totalPrice += orderItem.UnitPrice * orderItem.Quantity;
                order.OrderItems.Add(orderItem);
            }

            order.TotalPrice = totalPrice;

            logger.LogInformation("Creating order for customer {CustomerId}", request.CustomerId);

            await ordersRepository.AddAsync(order);
            return order.Id;
        }
    }

}
