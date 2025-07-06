using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Dishes.Queries.GetDishByCategoryIdForRestaurant
{
    public class GetDishesByCategoryIdForRestaurantQuery(int restaurantId, int categoryId) : IRequest<PagedResult<DishDto>>
    {
        public int RestaurantId { get; } = restaurantId;
        public int CategoryId { get; } = categoryId;
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
