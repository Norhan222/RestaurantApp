using Mapster;
using RestaurantApp.Application.Dtos;
using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Mapping
{
    public class MapsterConfig
    {
        public static void RegisterMapsterConfig()
        {
            TypeAdapterConfig<CategoryDto, Category>
                .NewConfig();
            TypeAdapterConfig<MenuItem,MenuItemDto>.NewConfig()
            .Map(src=>src.CategoryName,des=>des.Category.Name);
        }
    }
}
