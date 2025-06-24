using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;


//using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos
{
    public class DishesProfile : Profile
    {
        public DishesProfile()
        {
            CreateMap<Dish, DishDto>()
                 .ForMember(d => d.RestaurantName,
                    opt => opt.MapFrom(src => src.Restaurant.Name))
                  .ForMember(d => d.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateDishCommand, Dish>();
            CreateMap<UpdateDishCommand, Dish>();
        }
    }
}
