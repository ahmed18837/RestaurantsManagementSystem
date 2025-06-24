namespace Restaurants.Application.Orders.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;
        public List<OrderItemDto> Items { get; set; } = [];
    }
}
