using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger,
    ICustomersRepository customersRepository,
    ICustomerAuthorizationService customerAuthorizationService) : IRequestHandler<DeleteCustomerCommand>
    {
        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customersRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());

            if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Delete))
                throw new ForbidException();

            await customersRepository.DeleteAsync(customer);

            logger.LogInformation("Deleting Customer with id: {CustomerId}", request.Id);
        }
    }

}
