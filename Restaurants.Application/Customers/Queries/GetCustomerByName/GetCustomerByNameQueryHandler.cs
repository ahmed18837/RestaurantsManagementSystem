using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerByName
{
    public class GetCustomerByNameQueryHandler(ILogger<GetCustomerByNameQueryHandler> logger,
     ICustomersRepository customersRepository,
     IMapper mapper) : IRequestHandler<GetCustomerByNameQuery, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Customer {CustomerName}", request.Name);

            var customer = await customersRepository.GetByNameAsync(request.Name)
                    ?? throw new NotFoundNameException(nameof(Customer), request.Name);

            var customerDto = mapper.Map<CustomerDto>(customer);

            //restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

            return customerDto;
        }
    }
}
