using Microsoft.EntityFrameworkCore;
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
    public class CategoryRepo:GenericRepo<Category>,ICategoryRepo
    {
        private readonly RestaurantDbContext _dbContext;

        public CategoryRepo(RestaurantDbContext dbContext):base(dbContext)
        {
          _dbContext = dbContext;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
          return await _dbContext.Categories.AnyAsync(c=>c.Name.ToLower()== name.ToLower()&&!c.IsDeleted);
        }

        public async Task<IEnumerable<Category>> GetAllActiveAsync()
        {
            return await _dbContext.Categories.Where(c=>c.IsDeleted==false).ToListAsync();
        }
    }
}
