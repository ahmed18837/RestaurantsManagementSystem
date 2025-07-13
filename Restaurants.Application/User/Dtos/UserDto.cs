namespace Restaurants.Application.User.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string UserType { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public IEnumerable<string> Roles { get; set; } = [];
    }
}
