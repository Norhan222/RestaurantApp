using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.Dtos;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using RestaurantApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories
{
    public class MenuItemRepo:GenericRepo<MenuItem>,IMenuItemRepo
    {
        private readonly RestaurantDbContext _dbContext;

        public MenuItemRepo(RestaurantDbContext dbContext):base(dbContext)
        {
           _dbContext = dbContext;
        }

        public Task<bool> ExistsByNameAsync(string name)
        {
            return _dbContext.MenuItems.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync(int pageNumber, int pageSize)
        {
          return await  _dbContext.MenuItems.Where(m=>m.IsDeleted==false && m.Category.IsDeleted==false).Include(m => m.Category).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync()
        {
            return await _dbContext.MenuItems.Include(m => m.Category).ToListAsync();

        }

        //public async Task<IQueryable<MenuItem>> GetPagedMenuItemsAsync(int pageNumber, int pageSize)
        //{
        //    return  _dbContext.MenuItems.Skip((pageNumber-1)*pageSize).Take(pageSize);
        //}
    }
}
