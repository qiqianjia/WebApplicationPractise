using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> _lineCollection = new List<CartLine>();

        public void AddItem(Product P_Product, int P_Quatity)
        {
            CartLine line = _lineCollection
                .Where(p => p.Product.ProductID == P_Product.ProductID)
                .FirstOrDefault();
            if (line == null)
            {
                _lineCollection.Add(new CartLine { Product = P_Product, Quantity = P_Quatity });
            }
            else
            {
                line.Quantity += P_Quatity;
            }
        }

        public void RemoveLine(Product P_Product)
        {
            _lineCollection.RemoveAll(l => l.Product.ProductID == P_Product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return _lineCollection; }
        }

    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

}
