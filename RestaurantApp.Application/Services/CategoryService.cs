using Mapster;
using RestaurantApp.Application.Dtos;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Application.Results;
using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IFileService _fileService;

        public CategoryService(ICategoryRepo categoryRepo,IFileService fileService)
        {
           _categoryRepo = categoryRepo;
           _fileService = fileService;                                                      
        }
        public async Task<Result> CreateAsync(CreateCategoryDto categoryDto)
        {
            if(await _categoryRepo.ExistsByNameAsync(categoryDto.Name))
            {
                return Result.Fail("Category Name already exists");
            }
            if(categoryDto.Image !=null) 
               categoryDto.ImageUrl = await _fileService.UploadFile(categoryDto.Image, "Category");
           var category=categoryDto.Adapt<Category>();
           await _categoryRepo.CreateAsync(category);
           await _categoryRepo.SaveAsync();
           return Result.OK("Category Added Successfuly");

        }

        public async Task Delete(int id)
        {
            var category=await _categoryRepo.GetByIdAsync(id);
            if(category != null)
            {
                category.IsDeleted = true;
               await _categoryRepo.SaveAsync();
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
           var categories=await _categoryRepo.GetAllAsync();
            return categories.Adapt<IEnumerable<CategoryDto>>();
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return category.Adapt<CategoryDto>();
        }

        public async Task Update(EditCategoryDto categoryDto)
        {
            if(categoryDto.Image!=null)
            {
               await _fileService.DeleteFile(categoryDto.ImageUrl, "Category");
                categoryDto.ImageUrl = await _fileService.UploadFile(categoryDto.Image, "Category");
            }
            var category=categoryDto.Adapt<Category>();
            _categoryRepo.Update(category);
           await _categoryRepo.SaveAsync();
        }
    }
}
