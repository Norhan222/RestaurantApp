using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Interfaces
{
    public interface ICartService
    {
        public Task<CustomerCart?> GetCartAsync(string id);
        public Task<CustomerCart> AddToCartAsync(string key, CartItem cartitem);
        public Task Delete(string id);
        public Task DeleteFromCart(string key, int itemId);
        public Task<int> GetCartItmeCountAsync(string id);
        public Task MergeCartAsync(string sessionCartId, string userId);



    }
}
