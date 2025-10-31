using RestaurantApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Web.Models
{
    public class CustomerCartVM
    {
        public List<CartItem> CartItems { get; set; }
        [DataType(DataType.Currency)]
        public decimal SubTotal {  get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [DataType(DataType.Currency)]
        public decimal Shipping { get; set; } = 10;

    }
}
