using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler(ILogger<GetAllOrdersQueryHandler> logger,
     IMapper mapper,
     IOrdersRepository ordersRepository) : IRequestHandler<GetAllOrdersQuery, PagedResult<OrderDto>>
    {
        public async Task<PagedResult<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all orders");
            var (orders, totalCount) = await ordersRepository.GetAllMatchingAsync(request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            var ordersDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

            var result = new PagedResult<OrderDto>(ordersDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }
}
