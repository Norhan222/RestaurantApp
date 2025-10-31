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
            var pagedResult = await _menuItemService.GetAllAsync(pageNumber:1);
            var menuItemMapped = pagedResult.menuItems.Adapt<IEnumerable<MenuItemVM>>();
            var categories = await _categoryService.GetAllAsync();
            var categoryMapped = categories.Adapt<IEnumerable<CategoryVM>>();

            var menuvm = new MenuVM();
            menuvm.Categories = categoryMapped;
            menuvm.MenuItems = menuItemMapped;

            return View(menuvm);
        }
        public async  Task<IActionResult> AllMenu()
        {
            var Result = await _menuItemService.GetAllAsync(pageNumber:1);
            var menuItemMapped = Result.menuItems.Adapt<IEnumerable<MenuItemVM>>();
            var categories = await _categoryService.GetAllAsync();
            var categoryMapped = categories.Adapt<IEnumerable<CategoryVM>>();

            var menuvm = new MenuVM();
            menuvm.Categories = categoryMapped;
            menuvm.MenuItems = menuItemMapped;
            menuvm.PageCount = Result.PageCount;
            menuvm.PageNumber = Result.PageNumber;
            return View("Menu",menuvm);
        }

        public async Task<IActionResult> Menu(int page=1)
        {
            var Result = await _menuItemService.GetAllAsync(page);
            var menuItemMapped = Result.menuItems.Adapt<IEnumerable<MenuItemVM>>();
            var categories = await _categoryService.GetAllAsync();
            var categoryMapped = categories.Adapt<IEnumerable<CategoryVM>>();

            var menuvm = new MenuVM();
            menuvm.Categories = categoryMapped;
            menuvm.MenuItems = menuItemMapped;
            menuvm.PageCount = Result.PageCount;
            menuvm.PageNumber = Result.PageNumber;
            return PartialView("MenuItemPartial",menuvm);
        }
        public IActionResult Contact()
        {
            return View();
        }
  
    }
}
