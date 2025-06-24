using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler(ILogger<GetOrderByIdQueryHandler> logger,
     IOrdersRepository ordersRepository,
     IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting order {OrderId}", request.Id);

            var order = await ordersRepository.GetByIdIncludeWithOrderItemsAsync(request.Id)
                    ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

            var orderDto = mapper.Map<OrderDto>(order);

            return orderDto;
        }
    }
}
