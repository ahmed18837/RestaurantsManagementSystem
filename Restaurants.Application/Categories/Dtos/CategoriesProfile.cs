using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Categories.Dtos
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            //CreateMap<CreateDishCommand, Dish>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
