using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerByPhoneNumber
{
    public class GetCustomerByPhoneNumberQueryHandler(ILogger<GetCustomerByPhoneNumberQueryHandler> logger,
     IMapper mapper,
     ICustomerAuthorizationService customerAuthorizationService,
     ICustomersRepository customersRepository) : IRequestHandler<GetCustomerByPhoneNumberQuery, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var customer = await customersRepository.GetByPhoneNumberAsync(request.PhoneNumber)
                   ?? throw new NotFoundPhoneNumberException(nameof(Customer), request.PhoneNumber);

            if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Read))
                throw new ForbidException();

            var customerDto = mapper.Map<CustomerDto>(customer);


            logger.LogInformation("Getting Customer {CustomerPhoneNumber}", request.PhoneNumber);

            return customerDto;
        }
    }
}
