using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public interface IOrderProductsRepository
    {
        bool AddOrderProduct(OrderProduct orderProduct);
        bool DeleteOrderProduct(Guid orderID, Guid productID);
        bool DeleteOrderProduct(Guid orderID);
        bool UpdateOrderProduct(OrderProduct update);
        OrderProduct GetOrderProduct(Guid orderID, Guid productID);
        IEnumerable<OrderProduct> GetOrderProductsOfOrder(Guid orderID);
        IEnumerable<OrderProduct> GetOrderProducts();
    }
}
