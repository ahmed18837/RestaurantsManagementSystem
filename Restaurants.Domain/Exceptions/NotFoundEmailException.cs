namespace Restaurants.Domain.Exceptions
{
    public class NotFoundEmailException(string resourceType, string email)
    : Exception($"{resourceType} with Email : {email} doesn't exist")
    {
    }
}
