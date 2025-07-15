using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerByEmail
{
    public class GetCustomerByEmailQueryHandler(ILogger<GetCustomerByEmailQueryHandler> logger,
     ICustomersRepository customersRepository,
     ICustomerAuthorizationService customerAuthorizationService,
     IMapper mapper) : IRequestHandler<GetCustomerByEmailQuery, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
        {
            var customer = await customersRepository.GetByEmailAsync(request.Email)
                    ?? throw new NotFoundEmailException(nameof(Customer), request.Email);

            if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Read))
                throw new ForbidException();

            var customerDto = mapper.Map<CustomerDto>(customer);

            logger.LogInformation("Getting Customer {CustomerEmail}", request.Email);

            return customerDto;
        }
    }
}
