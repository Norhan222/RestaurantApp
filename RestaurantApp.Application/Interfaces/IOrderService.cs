using RestaurantApp.Application.Dtos;
using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Interfaces
{
    public interface IOrderService
    {
        public Task CreateOrder(string userId, string cartId,CreateOrderDto orderDto);

        public Task<Order> GetOrder(string userId);


    }
}
