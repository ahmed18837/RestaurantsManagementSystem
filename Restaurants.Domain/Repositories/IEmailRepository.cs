namespace Restaurants.Domain.Repositories
{
    public interface IEmailRepository
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }
}
