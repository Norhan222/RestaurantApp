using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Web.Models
{
    public class CreateCategoryVM
    {
        [Required(ErrorMessage ="Name Of Category Is Required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Image Of Category Is Required")]
        public IFormFile Image { get; set; }
    }
}
