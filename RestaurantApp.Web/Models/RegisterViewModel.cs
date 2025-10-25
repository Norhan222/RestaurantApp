using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Web.Models
{
	public class RegisterViewModel
	{
	
        [Required(ErrorMessage = "Username Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress(ErrorMessage ="Invaild Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Address Is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Password Dosenot match")]
        public string ConfirmPassword { get; set; }


    }
}
