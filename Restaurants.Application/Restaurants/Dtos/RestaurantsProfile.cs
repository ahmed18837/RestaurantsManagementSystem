using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
           .ForMember(d => d.City, opt =>
               opt.MapFrom(src => src.Address == null ? null : src.Address.City))
           .ForMember(d => d.PostalCode, opt =>
               opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
           .ForMember(d => d.Street, opt =>
               opt.MapFrom(src => src.Address == null ? null : src.Address.Street));

            CreateMap<UpdateRestaurantCommand, Restaurant>();

            CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }));

            CreateMap<Restaurant, RestaurantStatisticsDto>()
                .ForMember(d => d.RestaurantId,
                           opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.RestaurantName,
                           opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.DishesCount,
                           opt => opt.MapFrom(src => src.Dishes.Count))
                .ForMember(d => d.TotalRatings,
                           opt => opt.MapFrom(src => src.Ratings.Count))
                .ForMember(d => d.AverageRating,
                           opt => opt.MapFrom(src =>
                               src.Ratings.Any()
                               ? Math.Round(src.Ratings.Average(r => r.Stars), 2)
                               : 0));
        }
    }
}
