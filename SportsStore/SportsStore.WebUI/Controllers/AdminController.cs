using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;


namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository P_Repository)
        {
            _repository = P_Repository;
        }

        public ViewResult Index()
        {
            return View(_repository.Products);
        }

        //public ViewResult Edit(int P_ProductID)
        public ViewResult Edit(int ProductID)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID == ProductID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product P_Product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(P_Product);
                TempData["message"] = string.Format("{0} has been saved", P_Product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //数据值有错误
                return View(P_Product);
            }

        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

    }
}