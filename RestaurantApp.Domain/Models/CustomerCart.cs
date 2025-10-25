using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Models
{
    public class CustomerCart
    {
        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public CustomerCart(string id)
        {
            Id = id;
        }
    }
}
