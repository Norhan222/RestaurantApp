using RestaurantApp.Application.Dtos;
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

    }
}
