using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BookBook.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddProduct(Product product)
        {
            try
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Product GetProduct(Guid id)
        {
            return dbContext.Products.Find(id);
        }

        public Product GetProduct(string name)
        {
            return dbContext.Products.FirstOrDefault(p => p.Name == name);
        }

        public IEnumerable<Product> GetProducts()
        {
            return dbContext.Products.AsEnumerable();
        }

        public bool UpdateProduct(Product update)
        {
            if (update != null)
            {
                var find = dbContext.Products.Find(update.ID);
                if (find != null)
                {
                    find.Name = update.Name;
                    find.Price = update.Price;
                    find.Type = update.Type;

                    return true;
                }
            }

            return false;
        }
    }
}
