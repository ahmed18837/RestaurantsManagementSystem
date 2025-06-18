using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Customers.Dtos
{
    public class CustomersProfile : Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
