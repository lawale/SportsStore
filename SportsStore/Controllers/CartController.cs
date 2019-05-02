using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using SportsStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index(string returnUrl)
            => View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            var product = repository.Products
                .FirstOrDefault(p => p.Id == id);

            if(product != null)
            {
                var cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.Id == id);

            if(product != null)
            {
                var cart = GetCart();
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        Cart GetCart()
            => HttpContext.Session.GetJson<Cart>(nameof(Cart)) ?? new Cart();

        void SaveCart(Cart cart)
            => HttpContext.Session.SetJson(nameof(Cart), cart);
    }
}
