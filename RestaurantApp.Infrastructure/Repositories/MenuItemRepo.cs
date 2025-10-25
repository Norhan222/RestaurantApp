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

        public async Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync()
        {
          return await  _dbContext.MenuItems.Include(m => m.Category).ToListAsync();
        }
    }
}
