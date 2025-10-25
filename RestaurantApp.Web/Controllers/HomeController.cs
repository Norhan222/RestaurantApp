using System.Diagnostics;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Web.Models;

namespace RestaurantApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMenuItemService menuItemService,ICategoryService categoryService ,  ILogger<HomeController> logger)
        {
           _menuItemService = menuItemService;
           _categoryService = categoryService;
            _logger = logger;
        }
        public async  Task<IActionResult> Index()
        {
            var menuItems = await _menuItemService.GetAllAsync();
            var menuItemMapped = menuItems.Adapt<IEnumerable<MenuItemVM>>();
            var categories = await _categoryService.GetAllAsync();
            var categoryMapped = categories.Adapt<IEnumerable<CategoryVM>>();

            var menuvm = new MenuVM();
            menuvm.Categories = categoryMapped;
            menuvm.MenuItems = menuItemMapped;

            return View(menuvm);
        }

        public async Task<IActionResult> Menu()
        {
            var menuItems = await _menuItemService.GetAllAsync();
            var menuItemMapped = menuItems.Adapt<IEnumerable<MenuItemVM>>();
            var categories = await _categoryService.GetAllAsync();
            var categoryMapped = categories.Adapt<IEnumerable<CategoryVM>>();

            var menuvm = new MenuVM();
            menuvm.Categories = categoryMapped;
            menuvm.MenuItems = menuItemMapped;
            return View(menuvm);
        }
       
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
