using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services
{
    public class Cartservice : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly IMenuItemRepo _menuItemRepo;

        public Cartservice(ICartRepo cartRepo,IMenuItemRepo menuItemRepo)
        {
           _cartRepo = cartRepo;
           _menuItemRepo = menuItemRepo;
        }

        public async Task<CustomerCart> AddToCartAsync(string key, CartItem cartitem)
        {
            var cart=await _cartRepo.GetCartAsync(key) ??new CustomerCart(key);
            var existitem = cart.CartItems.FirstOrDefault(i => i.Id == cartitem.Id);
            if (existitem != null)
            {
                if(cartitem.Quantity>1)
                    existitem.Quantity = cartitem.Quantity;
               else existitem.Quantity +=cartitem.Quantity;
            }
            else
            {

                cart.CartItems.Add(cartitem);
            }
           var CustomerCart = await _cartRepo.UpdateCartAsync(cart);
            return CustomerCart;

        }


        public async Task Delete(string id)
        {
           await  _cartRepo.DeleteCartAsync(id);
        }

        public async Task DeleteFromCart(string key, int itemId)
        {
            var cart = await _cartRepo.GetCartAsync(key);
            var existitem = cart.CartItems.FirstOrDefault(i => i.Id == itemId);

            cart.CartItems.Remove(existitem);
            await _cartRepo.UpdateCartAsync(cart);

        }

        public async Task<CustomerCart?> GetCartAsync(string id)
        {
          return await  _cartRepo.GetCartAsync(id);
        }

        public async Task<int> GetCartItmeCountAsync(string id)
        {
           var cart= await _cartRepo.GetCartAsync(id);
            if (cart == null)
                return 0;
            return cart.CartItems.Count;
        }

        public async Task MergeCartAsync(string sessionCartId, string userId)
        {
            var sessionCart=await _cartRepo.GetCartAsync(sessionCartId);
            var userCart = await _cartRepo.GetCartAsync($"cart_user_{userId}");
            if (sessionCart == null)
                return;
            if(userCart == null)
            {
                userCart = new CustomerCart($"cart_user_{userId}");
            }
            foreach (var item in sessionCart.CartItems)
            {
                var existingitem = userCart.CartItems.FirstOrDefault(c => c.Id == item.Id);
                if (existingitem != null)
                {
                    existingitem.Quantity += item.Quantity;
                }
                else
                {
                    userCart.CartItems.Add(item);
                }
            }
            await _cartRepo.UpdateCartAsync(userCart);
            await _cartRepo.DeleteCartAsync(sessionCartId);
        }

        //public async Task<CustomerCart?> UpdateCartAsyn(int id)
        //{
        //    var item = await _menuItemRepo.GetByIdAsync(id);
        //    var existcart = await _cartRepo.GetCartAsync("cart1") ?? new CustomerCart("cart1");
        //    var existitem = existcart.CartItems.FirstOrDefault(i => i.Id == item.Id);
        //    if (existitem != null)
        //    {
        //        existitem.Quantity += 1;
        //    }
        //    else
        //    {

        //        existcart.CartItems.Add(new CartItem
        //        {
        //            Id = item.Id,
        //            Name = item.Name,
        //            ImageUrl = item.ImageUrl,
        //            Price = item.Price,
        //            Total = item.Price,
        //            Quantity = 1

        //        });
        //    }
        //    await  _cartRepo.UpdateCartAsync(existcart);
        //    return existcart;
        //}
    }
}
