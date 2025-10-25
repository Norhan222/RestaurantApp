using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Web.Models
{
    public class EditMenuItemVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Requird")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Price Is Requirdd")]
        [RegularExpression("^\\d{0,8}(\\.\\d{1,4})?$", ErrorMessage = "Price must be in decimal")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageUrl { get; set; }
  
        public IFormFile? Image { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}
