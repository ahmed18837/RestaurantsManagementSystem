using AutoMapper;
using Restaurants.Application.User.Commands.RegisterUser;
using Restaurants.Application.User.Commands.UpdateUser;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.User.Dtos
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserCommand, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

            CreateMap<ApplicationUser, UserDto>().ReverseMap();

            CreateMap<UpdateUserCommand, ApplicationUser>()
               .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
               .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
               .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName));
        }
    }
}
