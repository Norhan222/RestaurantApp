using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Web.Models;

public class LoginViewModel
{
	[Required(ErrorMessage = "Username Is Required")]
	public string UserName { get; set; }

	[Required(ErrorMessage = "Password Is Required")]
	[DataType(DataType.Password)]
	public string Password { get; set; }

    public bool RememberMe { get; set; }
}
