using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Orders.Dtos
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            // Order → OrderDto
            CreateMap<Order, OrderDto>();

            // OrderItem → OrderItemDto
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.DishName,
                    opt => opt.MapFrom(src => src.Dish.Name));  // مثال على خاصية مشتقة
        }
    }
}
