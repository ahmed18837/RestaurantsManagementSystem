using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryHandler(ILogger<GetOrdersByCustomerIdQueryHandler> logger,
     IMapper mapper,
     IOrdersRepository ordersRepository,
     ICustomersRepository customersRepository) : IRequestHandler<GetOrdersByCustomerIdQuery, CustomerOrdersDto>
    {
        public async Task<CustomerOrdersDto> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all orders By {CustomerId}", request.CustomerId);

            var customer = await customersRepository.GetByIdAsync(request.CustomerId)
                     ?? throw new NotFoundException(nameof(Customer), request.CustomerId.ToString());

            var (orders, totalCount) = await ordersRepository.GetAllMatchingAsync(request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            var ordersByCustomer = orders.Where(o => o.CustomerId == request.CustomerId);


            var pagedOrders = new PagedResult<OrderDto>(
                mapper.Map<List<OrderDto>>(ordersByCustomer),
                ordersByCustomer.Count(),
                request.PageSize,
                request.PageNumber);

            return new CustomerOrdersDto
            {
                Customer = mapper.Map<CustomerDto>(customer),
                OrdersPaged = pagedOrders
            };

        }
    }
}
