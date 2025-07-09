namespace Restaurants.Application.User.Dtos
{
    public class ResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public DateTime RefreshTokenExpiration { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
