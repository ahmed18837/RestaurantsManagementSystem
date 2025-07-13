using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System.Data;

namespace Restaurants.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger,
    ICustomersRepository customersRepository,
    IMapper mapper) : IRequestHandler<UpdateCustomerCommand>
    {
        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customersRepository.GetByIdAsync(request.Id)
                             ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());

            var customerWithSameEmail = await customersRepository.GetByEmailAsync(request.Email);
            if (customerWithSameEmail is not null && customerWithSameEmail.Id != request.Id)
            {
                throw new DuplicateNameException($"This email ({request.Email}) is already used by another customer.");
            }

            var customerWithSamePhone = await customersRepository.GetByPhoneNumberAsync(request.PhoneNumber);
            if (customerWithSamePhone is not null && customerWithSamePhone.Id != request.Id)
            {
                throw new DuplicateNameException($"This phone number ({request.PhoneNumber}) is already used by another customer.");
            }

            mapper.Map(request, customer);
            await customersRepository.SaveChanges();

            logger.LogInformation("Updating customer with ID: {CustomerId}", request.Id);
        }
    }

}
