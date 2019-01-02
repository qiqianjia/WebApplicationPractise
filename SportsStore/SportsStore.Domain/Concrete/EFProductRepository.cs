using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository:IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product P_Product)
        {
            if (P_Product.ProductID == 0)
            {
                context.Products.Add(P_Product);
            }
            else
            {
                Product dbEntry = context.Products.Find(P_Product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = P_Product.Name;
                    dbEntry.Description = P_Product.Description;
                    dbEntry.Price = P_Product.Price;
                    dbEntry.Category = P_Product.Category;
                }
            }
            context.SaveChanges();
        }
    }
}
