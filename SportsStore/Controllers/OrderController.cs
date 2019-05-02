using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public ViewResult Checkout() => View();

        [HttpPost]
        public ViewResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
                return View();
        }
    }
}
