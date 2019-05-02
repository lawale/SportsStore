﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider service)
        {
            var session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var cart = session?.GetJson<SessionCart>(nameof(Cart)) ?? new SessionCart();

            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson(nameof(Cart), this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson(nameof(Cart), this);
        }

        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove(nameof(Cart));
        }
    }
}
