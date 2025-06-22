using AutoMapper;
using Restaurants.Application.Categories.Commands.CreateCategory;
using Restaurants.Application.Categories.Commands.UpdateCategory;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Categories.Dtos
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
