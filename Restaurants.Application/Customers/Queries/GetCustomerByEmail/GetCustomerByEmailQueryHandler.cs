using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerByEmail
{
    public class GetCustomerByEmailQueryHandler(ILogger<GetCustomerByEmailQueryHandler> logger,
     ICustomersRepository customersRepository,
     IMapper mapper) : IRequestHandler<GetCustomerByEmailQuery, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Customer {CustomerEmail}", request.Email);

            var customer = await customersRepository.GetByEmailAsync(request.Email)
                    ?? throw new NotFoundEmailException(nameof(Customer), request.Email);

            var customerDto = mapper.Map<CustomerDto>(customer);

            return customerDto;
        }
    }
}
