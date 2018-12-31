using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string _sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //通过会话获取cart
            Cart cart = (Cart)controllerContext.HttpContext.Session[_sessionKey];
            //若会话中没有cart，则创建一个
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[_sessionKey] = cart;
            }

            //返回cart
            return cart;

        }
    }
}