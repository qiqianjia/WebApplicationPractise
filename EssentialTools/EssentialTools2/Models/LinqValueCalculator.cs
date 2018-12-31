using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools2.Models
{
    public class LinqValueCalculator:IValueCalculator
    {
        private IDiscountHelper discounter;

        public LinqValueCalculator(IDiscountHelper P_Discounter)
        {
            discounter = P_Discounter;
        }

        public decimal ValueProducts(IEnumerable<Product> P_ProductArray)
        {
            //return P_ProductArray.Sum(p => p.Price);
            return discounter.ApplyDiscount(P_ProductArray.Sum(p => p.Price));
        }
    }
}