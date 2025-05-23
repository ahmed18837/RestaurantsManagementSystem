using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    public class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : DbContext(options)
    {
        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<Dish> Dishes { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Rating> Ratings { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // تخصيص Sequence واحدة لجميع الـ IDs
            builder.HasSequence<int>("CommonSequence", schema: "dbo")
                .StartsAt(400)
                .IncrementsBy(4); // 4 الزيادة بمقدار 

            // Id start with 400 and increase with 4 for all Ids
            builder.Entity<Category>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");

            builder.Entity<Customer>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");

            builder.Entity<Dish>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");

            builder.Entity<Order>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");

            builder.Entity<OrderItem>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");

            builder.Entity<Rating>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");

            builder.Entity<Restaurant>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");


            // Restaurant - Address (One-to-One)
            builder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);

            // Restaurant - Dishes (One-to-Many)
            builder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne(d => d.Restaurant)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category - Dish (One-to-Many)
            builder.Entity<Category>()
                   .HasMany(c => c.Dishes)
                   .WithOne(d => d.Category)
                   .HasForeignKey(c => c.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            // OrderItem - Dish (Many-to-One)
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Dish)
                .WithMany()
                .HasForeignKey(oi => oi.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            // Dish - Ratings (One-to-Many)
            builder.Entity<Rating>()
                   .HasOne(r => r.Dish)
                   .WithMany(d => d.Ratings)
                   .HasForeignKey(r => r.DishId)
                   .HasForeignKey(r => r.DishId)
                   .OnDelete(DeleteBehavior.Cascade); // لما يمسح الطبق امسح تقييماته

            // Restaurant - Ratings (One-to-Many)
            builder.Entity<Rating>()
                .HasOne(r => r.Restaurant)
                .WithMany(r => r.Ratings)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict); // اختياري


            // Customer - Ratings (One-to-Many)
            builder.Entity<Customer>()
                   .HasMany(c => c.Ratings)
                   .WithOne(r => r.Customer)
                   .HasForeignKey(r => r.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Customer - Orders (One-to-Many)
            builder.Entity<Customer>()
                .HasMany(o => o.Orders)
                .WithOne(c => c.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // حذف العميل يحذف طلباته
            ;

            // Order - OrderItems (One-to-Many)
            builder.Entity<Order>()
                   .HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade); // حذف الطلب يحذف العناصر
            ;
        }
    }
}
