using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext P_RequestContext, Type P_ControllerType)
        {
            return P_ControllerType == null 
                ? null
                : (IController)ninjectKernel.Get(P_ControllerType);
        }

        private void AddBindings()
        {
            //Put bindings here
            /*
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>{
                new Product{ Name="Football",Price=25},
                new Product{ Name="Surf board",Price=179},
                new Product{ Name="Running shoes",Price=95}
            }.AsQueryable());
            ninjectKernel.Bind<IProductsRepository>().ToConstant(mock.Object);
            */

            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
        }


    }
}