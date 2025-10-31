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
    public interface IMenuItemRepo :IGenericRepo<MenuItem>
    {
        Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync(int pageNumber, int pageSize);
        Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync();

        Task<bool> ExistsByNameAsync(string name);
        //Task<IQueryable<MenuItem>> GetPagedMenuItemsAsync(int pageNumber, int pageSize);
    }
}
