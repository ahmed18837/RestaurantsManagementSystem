using AutoMapper;
//using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos
{
    public class DishesProfile : Profile
    {
        public DishesProfile()
        {
            //CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDto>()
                 .ForMember(d => d.RestaurantName,
                    opt => opt.MapFrom(src => src.Restaurant.Name))
                  .ForMember(d => d.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name)); ;
        }
    }
}
