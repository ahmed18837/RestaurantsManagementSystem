using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerByPhoneNumber
{
    public class GetCustomerByPhoneNumberQueryHandler(ILogger<GetCustomerByPhoneNumberQueryHandler> logger,
     IMapper mapper,
     ICustomersRepository customersRepository) : IRequestHandler<GetCustomerByPhoneNumberQuery, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Customer {CustomerPhoneNumber}", request.PhoneNumber);

            var customer = await customersRepository.GetByPhoneNumberAsync(request.PhoneNumber)
                   ?? throw new NotFoundPhoneNumberException(nameof(Customer), request.PhoneNumber);

            var customerDto = mapper.Map<CustomerDto>(customer);

            return customerDto;
        }
    }
}
