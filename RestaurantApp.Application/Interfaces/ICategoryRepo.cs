using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Interfaces
{
    public interface ICategoryRepo:IGenericRepo<Category>
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllActiveAsync();

    }
}
