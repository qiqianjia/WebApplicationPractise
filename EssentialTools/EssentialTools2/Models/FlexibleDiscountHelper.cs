using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools2.Models
{
    public class FlexibleDiscountHelper:IDiscountHelper
    {
        public decimal ApplyDiscount(decimal P_Total)
        {
            decimal discount = P_Total > 100 ? 70 : 25;
            return (P_Total - (discount / 100 * P_Total));
        }
    }
}