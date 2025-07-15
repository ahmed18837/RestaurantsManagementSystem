using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerByName
{
    public class GetCustomerByNameQueryHandler(ILogger<GetCustomerByNameQueryHandler> logger,
     ICustomersRepository customersRepository,
        ICustomerAuthorizationService customerAuthorizationService,
     IMapper mapper) : IRequestHandler<GetCustomerByNameQuery, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken)
        {
            var customer = await customersRepository.GetByNameAsync(request.Name)
                    ?? throw new NotFoundNameException(nameof(Customer), request.Name);

            if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Read))
                throw new ForbidException();

            var customerDto = mapper.Map<CustomerDto>(customer);

            logger.LogInformation("Getting Customer {CustomerName}", request.Name);

            return customerDto;
        }
    }
}
