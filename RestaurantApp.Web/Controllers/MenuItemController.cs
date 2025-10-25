using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp.Application.Dtos;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using RestaurantApp.Web.Models;
using System.Drawing.Printing;

namespace RestaurantApp.Web.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        public MenuItemController(IMenuItemService menuItemService, ICategoryService categoryService,IFileService fileService)
        {
            _menuItemService = menuItemService;
            _categoryService = categoryService;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var menuItems = await _menuItemService.GetAllAsync();
            var menuItemsmapped = menuItems.Adapt<IEnumerable<MenuItemVM>>();
            return View(menuItemsmapped);
        }

        public async Task<IActionResult> Create()
        {
            var menuItemVM = new CreateMenuItemVM();
            var categories = await _categoryService.GetAllAsync();
            menuItemVM.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();
            return View(menuItemVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMenuItemVM createMenuItemVM)
        {

            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();

                createMenuItemVM.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();
                return View(createMenuItemVM);
            }

            var menuItemdto =createMenuItemVM.Adapt<CreateMenuItemDto>();
            var result=await _menuItemService.CreateAsync(menuItemdto);
            if (!result.Success)
            {
                var categories = await _categoryService.GetAllAsync();

                createMenuItemVM.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();
                ModelState.AddModelError("", result.Message);
                return View(createMenuItemVM);
            }

                TempData["Message"] = result.Message;
                return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int id)
        {
            var menuItem=await _menuItemService.GetMenuItemAsync(id);
            var editmenuItemvm = menuItem.Adapt<EditMenuItemVM>();
            var categories = await _categoryService.GetAllAsync();
            editmenuItemvm.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();
            if (editmenuItemvm.IsDeleted)
                editmenuItemvm.IsDeleted = false;
            else editmenuItemvm.IsDeleted = true;
            var category = editmenuItemvm.Adapt<EditCategoryDto>();
            return View(editmenuItemvm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditMenuItemVM editMenuItemVM)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                editMenuItemVM.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();
                return View(editMenuItemVM);
            }
            if (editMenuItemVM.IsDeleted)
                editMenuItemVM.IsDeleted = false;
            else editMenuItemVM.IsDeleted = true;
            var meunItemDto=editMenuItemVM.Adapt<EditMenuItemDto>();
            await _menuItemService.Update(meunItemDto);
            return RedirectToAction(nameof(Index));
        }
    }
}
