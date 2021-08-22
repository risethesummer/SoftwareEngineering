using BookBook.Data;
using BookBook.Dtos.Order;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddOrder(Order order)
        {
            try
            {
                this.dbContext.Orders.Add(order);
                this.dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOrder(Guid id)
        {
            try
            {
                var find = this.dbContext.Orders.Find(id);
                if (find != null)
                {
                    this.dbContext.Orders.Remove(find);
                    this.dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Order> GetOrdersOfUser(Guid userID, bool isPurchased)
        {
            return this.dbContext.Orders.Where(o => o.UserID == userID && o.IsPurchased == isPurchased);
        }

        public Order GetOrder(Guid id)
        {
            return this.dbContext.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return this.dbContext.Orders.AsEnumerable();
        }

        public IEnumerable<Order> GetOrdersOfUser(Guid userID)
        {
            return this.dbContext.Orders.Where(o => o.UserID == userID);
        }

        public bool UpdateOrder(Order order)
        {
            try
            {
                var find = dbContext.Orders.Find(order.ID);
                if (find != null)
                {
                    find.TotalPrice = order.TotalPrice;
                    find.PurchasedTime = DateTime.Now;
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

        public bool PurchaseOrder(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
