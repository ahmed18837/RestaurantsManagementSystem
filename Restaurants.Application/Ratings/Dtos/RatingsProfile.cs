using AutoMapper;
using Restaurants.Application.Ratings.Commands.CreateRating;
using Restaurants.Application.Ratings.Commands.UpdateRating;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Ratings.Dtos
{
    public class RatingsProfile : Profile
    {
        public RatingsProfile()
        {
            CreateMap<Rating, RatingDto>()
                   .ForMember(d => d.RestaurantName,
                    opt => opt.MapFrom(src => src.Restaurant!.Name))
                   .ForMember(d => d.DishName,
                    opt => opt.MapFrom(src => src.Dish.Name))
                   .ForMember(d => d.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.Name));

            CreateMap<CreateRatingCommand, Rating>();

            CreateMap<UpdateRatingCommand, Rating>();
        }
    }
}
