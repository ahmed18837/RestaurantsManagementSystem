using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger,
    IOrderAuthorizationService orderAuthorizationService,
    IOrdersRepository ordersRepository) : IRequestHandler<DeleteOrderCommand>
    {
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await ordersRepository.GetByIdIncludeWithOrderItemsAsync(request.Id)
                        ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

            if (!orderAuthorizationService.CanModifyOrder(order))
                throw new ForbidException();

            logger.LogInformation("Deleting order with Id: {OrderId}", request.Id);

            await ordersRepository.DeleteAsync(order);
        }
    }

}
