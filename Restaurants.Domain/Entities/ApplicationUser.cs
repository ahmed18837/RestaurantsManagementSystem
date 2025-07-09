using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FullName { get; set; } = default!;

        public DateOnly? DateOfBirth { get; set; }

        [MaxLength(50)]
        public string UserType { get; set; } = default!;

        public int? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        [MaxLength(8)]
        public string? ResetCode { get; set; }

        [MaxLength(8)]
        public string? TwoFactorCode { get; set; }
        public DateTime? TwoFactorCodeExpiration { get; set; }
        public DateTime? TwoFactorSentAt { get; set; }

        public int TwoFactorAttempts { get; set; } = 0; // عدد المحاولات خلال الساعة
        public DateTime? LastTwoFactorAttempt { get; set; } // آخر وقت للمحاولة

        // 🔹 جديد: محاولات الفشل وقفل الحساب
        public int FailedTwoFactorAttempts { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; } // متى ينتهي القفل؟

        public List<RefreshToken> RefreshTokens { get; set; } = new();

        //public List<Restaurant> OwnedRestaurants { get; set; } = [];

        //migrationBuilder.Sql("UPDATE Restaurants " +
        //        "SET OwnerId = (SELECT TOP 1 Id FROM AspNetUsers)");
    }
}
