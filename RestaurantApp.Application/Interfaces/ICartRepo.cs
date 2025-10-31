using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Interfaces
{
    public interface ICartRepo
    {
        public Task<CustomerCart?> GetCartAsync(string id);

        public Task<CustomerCart?> UpdateCartAsync(CustomerCart cart);

        public Task DeleteCartAsync(string id);

    }
}
