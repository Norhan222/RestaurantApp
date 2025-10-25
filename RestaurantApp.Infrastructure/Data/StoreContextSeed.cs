using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(RestaurantDbContext dbContext)
        {
            if (!dbContext.Categories.Any())
            {
                var CategoyData = File.ReadAllText("../RestaurantApp.Infrastructure/Data/DataSeed/Categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(CategoyData);
                if (categories?.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        await dbContext.Categories.AddAsync(category);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

       
            if (!dbContext.MenuItems.Any())
            {
                var ItemsData = File.ReadAllText("../RestaurantApp.Infrastructure/Data/DataSeed/MenuItems.json");
                var Items = JsonSerializer.Deserialize<List<MenuItem>>(ItemsData);
                if (Items?.Count > 0)
                {
                    foreach (var Item in Items)
                    {
                        await dbContext.Set<MenuItem>().AddAsync(Item);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            
        }

    }
}
