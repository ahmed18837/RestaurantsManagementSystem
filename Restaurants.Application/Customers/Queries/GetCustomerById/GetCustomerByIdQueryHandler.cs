using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler(ILogger<GetCustomerByIdQueryHandler> logger,
     ICustomersRepository customersRepository,
     ICustomerAuthorizationService customerAuthorizationService,
     IMapper mapper) : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await customersRepository.GetByIdAsync(request.Id)
                    ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());

            if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Read))
                throw new ForbidException();

            var customerDto = mapper.Map<CustomerDto>(customer);

            //restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

            logger.LogInformation("Getting customer {CustomerId}", request.Id);

            return customerDto;
        }
    }
}
