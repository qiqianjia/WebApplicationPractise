using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools2.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal P_Total);
    }
    
    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal DiscountSize { get; set; }

        public DefaultDiscountHelper(decimal P_Discount)
        {
            this.DiscountSize = P_Discount;
        }

        public decimal ApplyDiscount(decimal P_Total)
        {
            //return (P_Total - (10m / 100m * P_Total));
            return (P_Total - (DiscountSize / 100m * P_Total));
        }
    }

}