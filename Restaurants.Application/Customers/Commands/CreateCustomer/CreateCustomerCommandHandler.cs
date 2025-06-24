using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System.Data;

namespace Restaurants.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger,
        IMapper mapper, ICustomersRepository customersRepository) : IRequestHandler<CreateCustomerCommand, int>
    {
        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new Customer {@Customer}", request);

            var existingCustomer = await customersRepository.GetByEmailAsync(request.Email);
            if (existingCustomer is not null)
            {
                throw new DuplicateNameException($"This Email : {request.Email} already existing!");
            }

            var customerWithSamePhone = await customersRepository.GetByPhoneNumberAsync(request.PhoneNumber);
            if (customerWithSamePhone is not null)
            {
                throw new DuplicateNameException($"This phone number ({request.PhoneNumber}) is already used.");
            }

            var customer = mapper.Map<Customer>(request);

            await customersRepository.AddAsync(customer);

            return customer.Id;
        }
    }

}
