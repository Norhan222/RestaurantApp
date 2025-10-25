using RestaurantApp.Domain.Enums;

namespace RestaurantApp.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> Items { get; set; }
        public Address Address { get; set; }
        public decimal SubTotal  { get; set; }//Items.Sum(i => i.Total);
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public OrderType OrderType { get; set; } = OrderType.Delivery;
        public decimal Discount { get; set; } = 0m;
        public decimal Tax { get; set; }     //=> SubTotal * 0.085m;
        public decimal GetTotal() => (SubTotal + Tax )- Discount;
    }
}