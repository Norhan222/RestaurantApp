using Mapster;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using RestaurantApp.Application.Dtos;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using RestaurantApp.Web.Models;
using System.Security.Claims;

namespace RestaurantApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public OrderController(ICartService cartService,IOrderService orderService )
        {
            _cartService = cartService;
           _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var key = $"cart_user_{userId}";
            var cart= await _cartService.GetCartAsync(key);
            var subtotal=cart.CartItems.Sum(i=>i.Price*i.Quantity);
            var tax = subtotal * 0.085m;
            var total = subtotal + tax;
            var createOrderDto = new CreateOrderVM();
            createOrderDto.Cart = cart;
            createOrderDto.Subtotal = subtotal;
            createOrderDto.Tax = tax;
            createOrderDto.Total = total;

            return View(createOrderDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var key = $"cart_user_{userId}";
                var orderDto = orderVM.Adapt<CreateOrderDto>();
                await  _orderService.CreateOrder(userId, key, orderDto);
                await _cartService.Delete(key);
                return RedirectToAction("Details");
            }
            return RedirectToAction("Index");
        }


    }
}
