using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

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
    }
}