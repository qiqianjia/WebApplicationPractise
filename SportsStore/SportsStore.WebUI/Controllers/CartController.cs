using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private IOrderProcessor _orderProcessor;

        /*
        public CartController(IProductRepository P_Repository)
        {
            _repository = P_Repository;
        }
        */

        public CartController(IProductRepository P_Repository, IOrderProcessor P_Processor)
        {
            _repository = P_Repository;
            _orderProcessor = P_Processor;
        }

        /*//修改程序，使用模型绑定器来获取session中的对象
        //public RedirectToRouteResult AddToCart(int P_ProductID, string P_ReturnUrl)
        public RedirectToRouteResult AddToCart(int ProductID, string ReturnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID == ProductID);
            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }

        //public RedirectToRouteResult RemoveFromCart(int P_ProductID, string P_ReturnUrl)
        public RedirectToRouteResult RemoveFromCart(int ProductID, string ReturnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID == ProductID);
            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        //public ViewResult Index(string P_ReturnUrl)
        public ViewResult Index(string ReturnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = ReturnUrl });
        }
    */

        public RedirectToRouteResult AddToCart(Cart P_Cart,int ProductID, string ReturnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID == ProductID);
            if (product != null)
            {
                P_Cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }

        //public RedirectToRouteResult RemoveFromCart(int P_ProductID, string P_ReturnUrl)
        public RedirectToRouteResult RemoveFromCart(Cart P_Cart, int ProductID, string ReturnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID == ProductID);
            if (product != null)
            {
                P_Cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }

        public ViewResult Index(Cart P_Cart,string ReturnUrl)
        {
            return View(new CartIndexViewModel {Cart = P_Cart, ReturnUrl = ReturnUrl});
        }

        /*
        public PartialViewResult Summary(Cart P_Cart)
        {
            return PartialView(P_Cart);
        }
        */

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        public ViewResult Summary(Cart P_Cart)
        {
            return View(P_Cart);
        }

        [HttpPost]
        public ViewResult Checkout(Cart P_Cart, ShippingDetails P_ShippingDetails)
        {
            if (P_Cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(P_Cart, P_ShippingDetails);
                P_Cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(P_ShippingDetails);
            }
        }


    }
}