using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Queries.GetOrdersByRestaurantId
{
    public class GetOrdersByRestaurantIdQueryHandler(ILogger<GetOrdersByRestaurantIdQueryHandler> logger,
     IMapper mapper,
     IOrdersRepository ordersRepository,
     IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetOrdersByRestaurantIdQuery, RestaurantOrdersDto>
    {
        public async Task<RestaurantOrdersDto> Handle(GetOrdersByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all orders By {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                     ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var (orders, totalCount) = await ordersRepository.GetAllMatchingAsync(request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            var ordersByRestaurant = orders.Where(o => o.OrderItems.Any(oi => oi.Dish.RestaurantId == request.RestaurantId));


            var pagedOrders = new PagedResult<OrderDto>(
                mapper.Map<List<OrderDto>>(ordersByRestaurant),
                ordersByRestaurant.Count(),
                request.PageSize,
                request.PageNumber);

            return new RestaurantOrdersDto
            {
                Restaurant = mapper.Map<RestaurantDto>(restaurant),
                OrdersPaged = pagedOrders
            };

        }
    }
}
