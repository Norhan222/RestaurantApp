using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Domain.Models
{
    public class MenuItem:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        //public ICollection<MenuItemImages> MenuItemImages { get; set; }
        [Range(0.01,10000)]
        public decimal Price { get; set; }
        //public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}