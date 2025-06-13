using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQuery(int restaurantId) : IRequest<PagedResult<DishDto>>
    {
        public int RestaurantId { get; } = restaurantId;
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }

}
