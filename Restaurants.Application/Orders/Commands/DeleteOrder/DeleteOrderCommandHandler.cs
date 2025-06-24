using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger,
    IOrdersRepository ordersRepository) : IRequestHandler<DeleteOrderCommand>
    {
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting order with Id: {OrderId}", request.Id);

            var order = await ordersRepository.GetByIdIncludeWithOrderItemsAsync(request.Id)
                        ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

            await ordersRepository.DeleteAsync(order);
        }
    }

}
