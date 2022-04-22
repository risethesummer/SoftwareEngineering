using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public interface IOrderRepository
    {
        bool AddOrder(Order order);
        bool DeleteOrder(Guid id);
        bool UpdateOrder(Order order);
        bool PurchaseOrder(Guid id);
        Order GetOrder(Guid id);
        IEnumerable<Order> GetOrdersOfUser(Guid userID);
        IEnumerable<Order> GetOrdersOfUser(Guid userID, bool isPurchased);
        IEnumerable<Order> GetOrders();
    }
}
