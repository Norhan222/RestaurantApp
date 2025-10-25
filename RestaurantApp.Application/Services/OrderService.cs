using RestaurantApp.Application.Dtos;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly IOrderRepo _orderRepo;

        public OrderService(ICartService cartService,IOrderRepo orderRepo)
        {
            _cartService = cartService;
           _orderRepo = orderRepo;
        }
        public async Task CreateOrder(string userId,string cartId ,CreateOrderDto orderDto)
        {
            var address = new Address()
            {
                FirstName = orderDto.FirstName,
                LastName = orderDto.LastName,
                Street = orderDto.Street,
                City = orderDto.City
            };
            var key = $"cart_user_{userId}";
            var cart = await _cartService.GetCartAsync(key);
            var subtotal = cart.CartItems.Sum(i => i.Quantity * i.Price);
            var tax = subtotal * 0.085m;
            var order = new Order()
            {
                UserId = userId,
                Address = address,
                SubTotal=subtotal,
                Tax=tax,
                Items = cart.CartItems.Select(i => new OrderItem
                {
                    MenuItemId = i.Id,
                    Name = i.Name,
                    Price = i.Price,
                    ImageUrl = i.ImageUrl,
                    Quantity = i.Quantity

                }).ToList(),
                
            };
          await  _orderRepo.AddOrder(order);
        }
    }
}
