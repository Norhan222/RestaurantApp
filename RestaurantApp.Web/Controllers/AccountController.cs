//using Foodie.DAL.Models;
//using Foodie.PL.Helper;
//using Foodie.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using RestaurantApp.Web.Models;
using System.Security.Claims;

namespace Foodie.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
        private readonly ICartService _cartService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ICartService cartService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
           _cartService = cartService;
        }
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user is not null)
				{
					ModelState.AddModelError(string.Empty, "UserName Is Already Exist");
				}

				var User = new AppUser
				{
					UserName = model.UserName,
					Email = model.Email,
					Address = model.Address,
				};
				var Result = await _userManager.CreateAsync(User, model.Password);
				if (Result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
				else
				{
					foreach (var error in Result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}

		public IActionResult Login()
		{
			var username = User.FindFirstValue(ClaimTypes.Name);
			if (username != null)
				return RedirectToAction("Index", "Home");
			else
				return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{

			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var Result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

						if (Result.Succeeded)
						{
							var key = HttpContext.Session.GetString("CartId");
							if (!string.IsNullOrEmpty(key))
							{
								await _cartService.MergeCartAsync($"cart_session_{key}", user.Id);
								HttpContext.Session.Remove("CartId");
							}
							var Roles = await _userManager.GetRolesAsync(user);
							if (Roles.Contains("Admin"))
								return RedirectToAction("Index", "Admin");
							else
								return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "InCorrect Password");

					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "UserName Is Not Exist");
				}

			}
			return View(model);
		}
		
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
	}
}
