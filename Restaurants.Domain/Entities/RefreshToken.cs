using Microsoft.EntityFrameworkCore;

namespace Restaurants.Domain.Entities
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.Now >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
