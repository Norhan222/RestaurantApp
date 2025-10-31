using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using RestaurantApp.Web.Models;
using System.Security.Claims;

namespace RestaurantApp.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IMenuItemRepo _menuItemRepo;

        public CartController(ICartService cartService,IMenuItemRepo menuItemRepo)
        {
          _cartService = cartService;
            _menuItemRepo = menuItemRepo;
        }
        public async Task<IActionResult> Index()
        {
            var key=GetCartKey();
            var cart= await _cartService.GetCartAsync(key);
            var cartVM = new CustomerCartVM();

            cartVM.CartItems = cart.CartItems;
            cartVM.SubTotal = cart.CartItems.Sum(c => c.Quantity * c.Price);
            cartVM.Total = cartVM.SubTotal + cartVM.Shipping;
            return View(cartVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var key = GetCartKey();
            var menuitem= await _menuItemRepo.GetByIdAsync(id);
            if(menuitem == null)
            {
                return NotFound();
            }
            var cartItem = new CartItem()
            {
                Id = menuitem.Id,
                Name = menuitem.Name,
                Price = menuitem.Price,
                ImageUrl = menuitem.ImageUrl,
                Quantity = 1
            };

            await _cartService.AddToCartAsync(key, cartItem);
            var  itemscount =await _cartService.GetCartItmeCountAsync(key);
            return Json(new { success=true, count=itemscount });
        }
        [HttpPost]
        public   async  Task<IActionResult> UpdateQuantity(int id,int quantity)
        {
            var key = GetCartKey();
            var menuitem = await _menuItemRepo.GetByIdAsync(id);
            if (menuitem == null)
            {
                return NotFound();
            }
            var cartItem = new CartItem()
            {
                Id = menuitem.Id,
                Name = menuitem.Name,
                Price = menuitem.Price,
                ImageUrl = menuitem.ImageUrl,
                Quantity = quantity
            };

           var updatedcartitem= await _cartService.AddToCartAsync(key, cartItem);
            var subtotal=updatedcartitem.CartItems.Sum(c=>c.Quantity * c.Price);
            
            return Json(new { success = true, newQuantity = quantity,subTotal=subtotal});

        }
        public async Task<IActionResult> GetCartCount()
        {
            var key = GetCartKey();
            var countitem = await _cartService.GetCartItmeCountAsync(key);
            return Json(new { success = false, count = countitem });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var key = GetCartKey();
            await _cartService.DeleteFromCart(key,id);
            return RedirectToAction("Index");

        }
        private string GetCartKey()
        {
            if(User.Identity!=null && User.Identity.IsAuthenticated)
            {
                return $"cart_user_{User.FindFirstValue(ClaimTypes.NameIdentifier)}";
            }
            if (HttpContext.Session.GetString("CartId") == null)
                HttpContext.Session.SetString("CartId", Guid.NewGuid().ToString());
            return $"cart_session_{HttpContext.Session.GetString("CartId")}";
        }
    }
}
