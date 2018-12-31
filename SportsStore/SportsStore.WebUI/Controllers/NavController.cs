using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;


namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository _repository;

        public NavController(IProductRepository P_Repository)
        {
            _repository = P_Repository;
        }

        public PartialViewResult Menu(string P_Category=null)
        {
            ViewBag.SelectedCategory = P_Category;
            IEnumerable<string> categories = _repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}