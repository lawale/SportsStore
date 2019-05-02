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
        Cart cart;

        public CartController(IProductRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        public ViewResult Index(string returnUrl)
            => View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            var product = repository.Products
                .FirstOrDefault(p => p.Id == id);

            if(product != null)
                cart.AddItem(product, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
                cart.RemoveLine(product);
            return RedirectToAction("Index", new { returnUrl });
        }
        
    }
}
