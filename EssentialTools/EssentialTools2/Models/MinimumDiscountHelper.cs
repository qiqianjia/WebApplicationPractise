using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools2.Models
{
    public class MinimumDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal P_Total)
        {
            if (P_Total < 0)
            { throw new ArgumentOutOfRangeException(); }
            else if (P_Total > 100)
            { return P_Total * 0.9M; }
            else if (P_Total >= 10 && P_Total <= 100)
            { return P_Total - 5; }
            else
            { return P_Total; }
        }
    }
}