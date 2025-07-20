using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler(ILogger<GetOrderByIdQueryHandler> logger,
     IOrdersRepository ordersRepository,
     IOrderAuthorizationService orderAuthorizationService,
     IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await ordersRepository.GetByIdIncludeWithOrderItemsAsync(request.Id)
                    ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

            if (!orderAuthorizationService.CanViewCustomerOrder(order.Customer.ApplicationUserId))
                throw new ForbidException();

            logger.LogInformation("Getting order {OrderId}", request.Id);

            var orderDto = mapper.Map<OrderDto>(order);

            return orderDto;
        }
    }
}
