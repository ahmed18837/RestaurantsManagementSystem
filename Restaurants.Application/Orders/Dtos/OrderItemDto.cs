namespace Restaurants.Application.Orders.Dtos
{

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // معلومات اختيارية إضافيّة
        public int DishId { get; set; }
        public string DishName { get; set; } = default!;
    }
}
