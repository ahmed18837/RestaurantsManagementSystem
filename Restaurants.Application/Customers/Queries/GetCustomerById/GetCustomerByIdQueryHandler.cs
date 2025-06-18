using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler(ILogger<GetCustomerByIdQueryHandler> logger,
     ICustomersRepository customersRepository,
     IMapper mapper) : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting customer {CustomerId}", request.Id);
            var customer = await customersRepository.GetByIdAsync(request.Id)
                    ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());

            var customerDto = mapper.Map<CustomerDto>(customer);

            //restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

            return customerDto;
        }
    }
}
