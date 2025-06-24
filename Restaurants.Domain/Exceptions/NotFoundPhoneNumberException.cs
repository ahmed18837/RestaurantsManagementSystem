namespace Restaurants.Domain.Exceptions
{
    public class NotFoundPhoneNumberException(string resourceType, string phoneNumber)
   : Exception($"{resourceType} with PhoneNumber : {phoneNumber} doesn't exist")
    {
    }
}
