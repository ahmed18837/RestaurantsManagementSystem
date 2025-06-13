namespace Restaurants.Application.Dishes.Dtos
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageFileName { get; set; }
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }

        //public static DishDto FromEntity(Dish dish)
        //{
        //    return new DishDto()
        //    {
        //        Id = dish.Id,
        //        Name = dish.Name,
        //        Description = dish.Description,
        //        Price = dish.Price,
        //        KiloCalories = dish.KiloCalories,
        //    };
        //}

    }
}
