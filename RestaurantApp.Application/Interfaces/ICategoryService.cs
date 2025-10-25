using RestaurantApp.Application.Dtos;
using RestaurantApp.Application.Results;
using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<Result> CreateAsync(CreateCategoryDto categoryDto);
        Task Update(EditCategoryDto categoryDto);
        Task Delete(int id);

    }
}
