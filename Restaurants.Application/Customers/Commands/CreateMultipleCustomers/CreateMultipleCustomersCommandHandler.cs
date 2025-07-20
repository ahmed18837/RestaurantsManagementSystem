using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Customers.Commands.CreateMultipleCustomers
{
    public class CreateMultipleCustomersCommandHandler(
       ILogger<CreateMultipleCustomersCommandHandler> logger,
       IMapper mapper,
       ICustomersRepository customersRepository,
       UserManager<ApplicationUser> userManager,
       IEmailService emailService)
       : IRequestHandler<CreateMultipleCustomersCommand, List<int>>
    {
        public async Task<List<int>> Handle(CreateMultipleCustomersCommand request, CancellationToken cancellationToken)
        {
            var createdCustomerIds = new List<int>();

            foreach (var customerCommand in request.Customers)
            {
                logger.LogInformation("Creating customer {@Customer}", customerCommand);

                // تحقق من وجود العميل مسبقًا
                var existingCustomer = await customersRepository.GetByEmailAsync(customerCommand.Email);
                if (existingCustomer is not null)
                {
                    logger.LogWarning("Email {Email} already exists. Skipping...", customerCommand.Email);
                    continue;
                }

                var customerWithSamePhone = await customersRepository.GetByPhoneNumberAsync(customerCommand.PhoneNumber);
                if (customerWithSamePhone is not null)
                {
                    logger.LogWarning("Phone number {PhoneNumber} already used. Skipping...", customerCommand.PhoneNumber);
                    continue;
                }

                var user = new ApplicationUser
                {
                    FullName = customerCommand.Name,
                    UserName = customerCommand.Email,
                    Email = customerCommand.Email,
                    PhoneNumber = customerCommand.PhoneNumber,
                    UserType = UserRoles.User
                };

                var result = await userManager.CreateAsync(user, customerCommand.Password);
                if (!result.Succeeded)
                {
                    logger.LogWarning("Failed to create user for {Email}: {Errors}", customerCommand.Email,
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    continue;
                }

                await userManager.AddToRoleAsync(user, UserRoles.User);

                var customer = mapper.Map<Customer>(customerCommand);
                customer.ApplicationUserId = user.Id;

                try
                {
                    await customersRepository.AddAsync(customer);

                    user.CustomerId = customer.Id;
                    await userManager.UpdateAsync(user);

                    createdCustomerIds.Add(customer.Id);

                    // إرسال بريد إلكتروني
                    var subject = "Welcome to the Restaurants System - Your Login Details";
                    var body = $@"
                    <html>
                    <body>
                        <p>Hello {customer.Name},</p>
                        <p>Your account has been created successfully.</p>
                        <p>Email: {customer.Email}</p>
                        <p>Password: {customerCommand.Password}</p>
                    </body>
                    </html>";

                    await emailService.SendEmailAsync(customer.Email, subject, body);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while creating customer {Email}. Rolling back.", customer.Email);
                    await userManager.DeleteAsync(user);
                }
            }

            return createdCustomerIds;
        }
    }

}
