using RestaurantApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Web.Models
{
    public class CreateOrderVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public CustomerCart? Cart { get; set; }
    }
}
