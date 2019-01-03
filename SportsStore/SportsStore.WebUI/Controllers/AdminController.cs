using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;


namespace SportsStore.WebUI.Controllers
{
    [Authorize]
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

        /*
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
        */

        [HttpPost]
        public ActionResult Edit(Product P_Product,HttpPostedFileBase P_Image)
        {
            if (ModelState.IsValid)
            {
                if (P_Image != null)
                {
                    P_Product.ImageMimeType = P_Image.ContentType;
                    P_Product.ImageData = new byte[P_Image.ContentLength];
                    P_Image.InputStream.Read(P_Product.ImageData, 0, P_Image.ContentLength);
                }
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

        [HttpPost]
        public ActionResult Delete(int P_ProductID)
        {
            Product deletedProduct = _repository.DeleteProduct(P_ProductID);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }


    }
}