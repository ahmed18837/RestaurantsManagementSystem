using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger,
    ICustomersRepository customersRepository) : IRequestHandler<DeleteCustomerCommand>
    {
        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Customer with id: {CustomerId}", request.Id);

            var customer = await customersRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());

            await customersRepository.DeleteAsync(customer);
        }
    }

}
