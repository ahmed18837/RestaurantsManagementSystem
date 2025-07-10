using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

using Restaurants.Domain.Repositories;
using System.Data;

namespace Restaurants.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger,
        IMapper mapper, ICustomersRepository customersRepository,
         UserManager<ApplicationUser> userManager,
          IEmailService emailService) : IRequestHandler<CreateCustomerCommand, int>
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

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserType = UserRoles.User
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user: {errors}");
            }

            await userManager.AddToRoleAsync(user, UserRoles.User);

            var customer = mapper.Map<Customer>(request);
            customer.ApplicationUserId = user.Id;
            try
            {
                await customersRepository.AddAsync(customer);

                user.CustomerId = customer.Id;
                await userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                await userManager.DeleteAsync(user);
                throw new Exception("Failed to create the customer. The operation has been rolled back. " + ex.Message);
            }

            string subject = "Welcome to the Restaurants System - Your Login Details";

            string body = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Welcome to Restaurants System</title>
</head>
<body style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 0; margin: 0;'>

    <div style='max-width: 600px; margin: 40px auto; background-color: #fff; padding: 30px; border-radius: 10px; box-shadow: 0 0 15px rgba(0,0,0,0.05);'>

        <h2 style='color: #2E86C1;'>Welcome to Restaurants System</h2>

        <p>Hello <strong>{customer.Name}</strong>,</p>

        <p>Your account has been created successfully. Please log in using the credentials below:</p>

        <div style='background-color: #f0f8ff; padding: 20px; border-left: 5px solid #2E86C1; border-radius: 6px; margin: 20px 0;'>
            <p><strong>Email:</strong> {customer.Email}</p>
            <p><strong>Password:</strong> {request.Password}</p>
        </div>

        <p><strong>Please log in to start using the system.</strong></p>

        <p>If you did not request this account, you can safely ignore this message.</p>

        <p style='margin-top: 30px;'>Best regards,<br><strong>Restaurants System Team</strong></p>

        <hr style='margin-top: 40px;' />
        <p style='font-size: 13px; color: #aaa; text-align: center;'>&copy; 2025 Restaurants System. All rights reserved.</p>

    </div>

</body>
</html>";

            try
            {
                await emailService.SendEmailAsync(customer.Email, subject, body);
            }
            catch (Exception ex)
            {
                logger.LogWarning("Customer was created, but sending the email failed: {Message}", ex.Message);
            }

            return customer.Id;
        }
    }

}
