using AutoMapper;
using Restaurants.Application.Orders.Commands.CreateOrder;
using Restaurants.Application.Orders.Commands.UpdateOrder;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Orders.Dtos
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.TotalPrice,
                       opt => opt.MapFrom(s => s.Quantity * s.UnitPrice))
            .ForMember(d => d.DishName,
                       opt => opt.MapFrom(s => s.Dish.Name));

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Items,
                           opt => opt.MapFrom(s => s.OrderItems))
                .ForMember(d => d.CustomerName,
                           opt => opt.MapFrom(s => s.Customer.Name))
                .ForMember(d => d.TotalPrice,
                           opt => opt.MapFrom(s =>
                               s.OrderItems.Sum(i => i.Quantity * i.UnitPrice)))
                .ForMember(d => d.Quantity,
                           opt => opt.MapFrom(s =>
                               s.OrderItems.Sum(i => i.Quantity)));


            CreateMap<CreateOrderItemDto, OrderItem>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.UnitPrice, opt => opt.Ignore());

            CreateMap<CreateOrderCommand, Order>()
               .ForMember(d => d.Id, opt => opt.Ignore())
               .ForMember(d => d.OrderDate, opt => opt.Ignore())
               .ForMember(d => d.TotalPrice, opt => opt.Ignore())
               .ForMember(d => d.OrderItems, opt => opt.Ignore());

            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.OrderDate, opt => opt.Ignore())
                .ForMember(d => d.TotalPrice, opt => opt.Ignore())
                .ForMember(d => d.OrderItems, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, _, srcMember) => srcMember is not null));
        }
    }
}
