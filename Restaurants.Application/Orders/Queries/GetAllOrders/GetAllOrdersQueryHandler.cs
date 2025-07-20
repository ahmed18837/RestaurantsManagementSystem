using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler(ILogger<GetAllOrdersQueryHandler> logger,
     IMapper mapper,
     IOrdersRepository ordersRepository,
     IOrderAuthorizationService orderAuthorizationService
      ) : IRequestHandler<GetAllOrdersQuery, PagedResult<OrderDto>>
    {
        public async Task<PagedResult<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var (orders, totalCount) = await ordersRepository.GetAllMatchingAsync(request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            if (!orderAuthorizationService.CanViewAllOrders())
                throw new ForbidException();

            logger.LogInformation("Getting all orders");

            var ordersDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

            var result = new PagedResult<OrderDto>(ordersDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }
}
