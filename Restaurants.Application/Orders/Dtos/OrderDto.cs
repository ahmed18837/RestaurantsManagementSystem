namespace Restaurants.Application.Orders.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; } = [];
    }
}
