using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Syntax;
using Ninject.Parameters;
using System.Configuration;
using EssentialTools2.Models;
using System.Web.Mvc;

namespace EssentialTools2.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {

        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        
        public object GetService(Type P_ServiceType)
        {
            return kernel.TryGet(P_ServiceType);
        }

        public IEnumerable<object> GetServices(Type P_ServiceTypeArray)
        {
            return kernel.GetAll(P_ServiceTypeArray);
        }

        public void AddBindings()
        {
            kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>();
            //kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>()
            //    .WithPropertyValue("DiscountSize", 50M);
            kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>()
                .WithConstructorArgument("P_Discount", 50M);
            kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>()
                .WhenInjectedInto<LinqValueCalculator>();
        }
    }
}