using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    [Owned]
    public class Address
    {
        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Street { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }
    }
}
