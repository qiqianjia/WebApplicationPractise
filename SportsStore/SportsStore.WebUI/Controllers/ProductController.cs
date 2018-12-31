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
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 4;

        public ProductController(IProductRepository P_ProductRespoistory)
        {
            this._repository = P_ProductRespoistory;
        }

        
        //public ViewResult List(int P_Page = 1)
        public ViewResult List(string P_Category,int P_Page=1)
        {
            /*
            return View(_repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((P_Page - 1) * PageSize)
                .Take(PageSize)
                );
                */
            /*
        ProductsListViewModel model = new ProductsListViewModel
        {
            Products = _repository.Products
        .OrderBy(p => p.ProductID)
        .Skip((P_Page - 1) * PageSize)
        .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = P_Page,
                ItemsPerPage = PageSize,
                TotalItems = _repository.Products.Count()
            }
        };
        */

            /*
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(p => P_Category == null || p.Category == P_Category)
            .OrderBy(p => p.ProductID)
            .Skip((P_Page - 1) * PageSize)
            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = P_Page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                },
                CurrentCategory = P_Category
            };
            */

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(p => P_Category == null || p.Category == P_Category)
            .OrderBy(p => p.ProductID)
            .Skip((P_Page - 1) * PageSize)
            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = P_Page,
                    ItemsPerPage = PageSize,
                    TotalItems = P_Category==null?_repository.Products.Count()
                    :_repository.Products.Where(e=>e.Category==P_Category).Count()
                },
                CurrentCategory = P_Category
            };

            return View(model);
        }
    

    }
}