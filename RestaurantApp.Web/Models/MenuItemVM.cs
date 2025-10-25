using RestaurantApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Web.Models
{
    public class MenuItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
