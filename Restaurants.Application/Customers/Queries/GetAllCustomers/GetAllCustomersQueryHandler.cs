using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler(ILogger<GetAllCustomersQueryHandler> logger,
     IMapper mapper,
     ICustomerAuthorizationService customerAuthorizationService,
     ICustomersRepository customersRepository) : IRequestHandler<GetAllCustomersQuery, PagedResult<CustomerDto>>
    {
        public async Task<PagedResult<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            if (!customerAuthorizationService.Authorize(new(), ResourceOperation.Read))
                throw new ForbidException();

            var (customers, totalCount) = await customersRepository.GetAllMatchingAsync(request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            var customersDtos = mapper.Map<IEnumerable<CustomerDto>>(customers);

            var result = new PagedResult<CustomerDto>(customersDtos, totalCount, request.PageSize, request.PageNumber);

            logger.LogInformation("Getting all customers");

            return result;
        }
    }
}
