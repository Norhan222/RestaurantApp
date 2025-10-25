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
    public class OrderRepo : IOrderRepo
    {
        private readonly RestaurantDbContext _dbContext;

        public OrderRepo(RestaurantDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task AddOrder(Order order)
        {
           await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
