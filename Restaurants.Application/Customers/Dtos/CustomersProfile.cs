using AutoMapper;
using Restaurants.Application.Customers.Commands.CreateCustomer;
using Restaurants.Application.Customers.Commands.UpdateCustomer;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Customers.Dtos
{
    public class CustomersProfile : Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();
        }
    }
}
