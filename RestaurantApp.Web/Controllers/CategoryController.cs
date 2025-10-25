using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.Dtos;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using RestaurantApp.Infrastructure.Data;
using RestaurantApp.Web.Helper;
using RestaurantApp.Web.Models;

namespace RestaurantApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;

        public CategoryController(ICategoryService categoryService,IFileService fileService)
        {
            _categoryService = categoryService;
           _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesmapped=categories.Adapt<IEnumerable<CategoryVM>>();
            return View(categoriesmapped);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM createCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createCategoryVM);
            }
            //var fileInfo = _fileService.UploadFile(createCategoryVM.Image, "Category");
            //createCategoryVM.ImageUrl = fileInfo.filename;
            
            var category = createCategoryVM.Adapt<CreateCategoryDto>();
            var result=  await _categoryService.CreateAsync(category);
            if(!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                 return View(createCategoryVM);
            }
            TempData["Message"]=result.Message;
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var category=await _categoryService.GetByIdAsync(id);
            var categoryvm=category.Adapt<EditCategoryVM>();
            if(categoryvm.IsDeleted)
                categoryvm.IsDeleted = false;
            else categoryvm.IsDeleted = true;
            return View(categoryvm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryVM editCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editCategoryVM);
            }
      
            if (editCategoryVM.IsDeleted)
                editCategoryVM.IsDeleted = false;
            else editCategoryVM.IsDeleted = true;
            var category = editCategoryVM.Adapt<EditCategoryDto>();
             await _categoryService.Update(category);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        { 
            await _categoryService.Delete(id);
            return RedirectToAction("Index");
        }



    }
}
