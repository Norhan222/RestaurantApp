using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Web.Models
{
    public class EditCategoryVM
    {
        [Required(ErrorMessage = "Name Of Category Is Required")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get ; set; }
        public IFormFile? Image { get; set; }

    }
}
