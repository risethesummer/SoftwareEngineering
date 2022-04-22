using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public interface IProductRepository
    {
        bool AddProduct(Product product);
        bool UpdateProduct(Product update);
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts();
        Product GetProduct(string name);
    }
}
