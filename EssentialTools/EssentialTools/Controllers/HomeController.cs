﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;

namespace EssentialTools.Controllers
{
    public class HomeController:Controller
    {
        private Product[] _productArray = {
            new Product{ Name="Kayak",Category="Watersports",Price=275M},
            new Product{ Name="Lifejacket",Category="Watersports",Price=48.95M},
            new Product{ Name="Soccer ball",Category="Soccer",Price=19.50M},
            new Product{ Name="Corner flag",Category="Soccer",Price=34.95M}
        };

        public ActionResult Index()
        {
            LinqValuCalculator calc = new LinqValuCalculator();
            ShoppingCart cart = new ShoppingCart(calc) { ProductArray = _productArray };
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}