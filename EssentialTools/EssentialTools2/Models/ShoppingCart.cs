using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools2.Models
{
    public class ShoppingCart
    {
        //private LinqValuCalculator calc;
        private IValueCalculator calc;

        //public ShoppingCart(LinqValuCalculator P_CalcParam)
        public ShoppingCart(IValueCalculator P_CalcParam)
        {
            calc = P_CalcParam;
        }

        public IEnumerable<Product> ProductArray { get; set; }

        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(ProductArray);
        }
    }
}