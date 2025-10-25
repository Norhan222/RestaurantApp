using Microsoft.Extensions.Caching.Distributed;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly IDatabase _database;

        public CartRepo(IConnectionMultiplexer redis)
        {
           _database = redis.GetDatabase();
        }
        public async Task DeleteCartAsync(string id)
        {
           await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerCart?> GetCartAsync(string id)
        {
            var cart = await _database.StringGetAsync(id);
           
            return cart.IsNull ? null:JsonSerializer.Deserialize<CustomerCart>(cart);
        }

        public async Task UpdateCartAsync(CustomerCart cart)
        {
            var jsonCart = JsonSerializer.Serialize(cart);
            await _database.StringSetAsync(cart.Id, jsonCart,TimeSpan.FromDays(1));
        }
    }
}
