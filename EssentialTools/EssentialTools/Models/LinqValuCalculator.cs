using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValuCalculator
    {
        public decimal ValueProducts(IEnumerable<Product> P_ProductArray)
        {
            return P_ProductArray.Sum(p => p.Price);
        }
    }
}