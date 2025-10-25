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
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemDto>> GetAllAsync();
        Task<MenuItemDto> GetMenuItemAsync(int id);
        Task<Result> CreateAsync(CreateMenuItemDto createMenuItemDto);
        Task Update(EditMenuItemDto categoryDto);
        Task Delete(int id);

    }
}
