using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public class OrderProductsRepository : IOrderProductsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderProductsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddOrderProduct(OrderProduct orderProduct)
        {
            try
            {
                dbContext.OrderProduct.Add(orderProduct);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOrderProduct(Guid orderID, Guid productID)
        {
            try
            {
                var find  = dbContext.OrderProduct.Find(orderID, productID);
                if (find != null)
                {
                    dbContext.OrderProduct.Remove(find);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOrderProduct(Guid orderID)
        {
            try
            {
                var find = dbContext.OrderProduct.Where(o => o.OrderID == orderID);
                if (find != null)
                {
                    dbContext.OrderProduct.RemoveRange(find);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public OrderProduct GetOrderProduct(Guid orderID, Guid productID)
        {
            return dbContext.OrderProduct.Find(orderID, productID);
        }

        public IEnumerable<OrderProduct> GetOrderProducts()
        {
            return dbContext.OrderProduct.AsEnumerable();
        }

        public IEnumerable<OrderProduct> GetOrderProductsOfOrder(Guid orderID)
        {
            return dbContext.OrderProduct.Where(o => o.OrderID == orderID);
        }

        public bool UpdateOrderProduct(OrderProduct update)
        {
            try
            {
                var find = dbContext.OrderProduct.Find(update.OrderID, update.ProductID);
                if (find != null)
                {
                    find.Amount = update.Amount;
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
