using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Repositories;
using BookBook.Dtos.Order;
using BookBook.Manager;
using BookBook.Models;
using BookBook.Dtos;

namespace BookBook.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductsRepository orderProductsRepository;
        private readonly ITheaterProductRepository theaterProductRepository;
        private readonly IUserActivitiesManager userActivitiesManager;
        private readonly IProductRepository productRepository;

        public OrderController(IOrderProductsRepository orderProductsRepository, IOrderRepository orderRepository,
            ITheaterProductRepository theaterProductRepository)
        {

        }

        [HttpGet]
        public IEnumerable<OrderDto> GetOrders()
        {
            foreach (Order order in orderRepository.GetOrders())
            {
                var oPs = orderProductsRepository.GetOrderProductsOfOrder(order.ID);
                yield return order.AsDto(oPs);
            }
        }
        
        [HttpGet("notPurchased/{id}")]
        public IEnumerable<OrderDto> GetNotPurchasedOrders(Guid userId)
        {
            foreach (Order order in orderRepository.GetOrdersOfUser(userId, false))
            {
                var oPs = orderProductsRepository.GetOrderProductsOfOrder(order.ID);
                yield return order.AsDto(oPs);
            }
        }

        [HttpGet("purchased/{id}")]
        public IEnumerable<OrderDto> GetPurchasedOrders(Guid userId)
        {
            foreach (Order order in orderRepository.GetOrdersOfUser(userId, true))
            {
                var oPs = orderProductsRepository.GetOrderProductsOfOrder(order.ID);
                yield return order.AsDto(oPs);
            }
        }



        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(Guid id)
        {
            try
            {
                var order = orderRepository.GetOrder(id);
                if (order != null)
                {
                    orderProductsRepository.DeleteOrderProduct(id);
                    orderRepository.DeleteOrder(id);
                    return Ok(new JsonResponse() { State = "Success" });
                }
                return BadRequest(new JsonResponse() { State = "Fail" });
            }
            catch
            {
                return BadRequest(new JsonResponse() { State = "Fail" });
            }
        }

        [HttpPost]
        public ActionResult CreateOrder(CreateOrderDto create)
        {
            if (userActivitiesManager.IsValidSession(create.SessionID))
            {
                var userID = userActivitiesManager.GetUserId(create.SessionID);

                List<Guid> cantBuy = new();
                
                long totalPrice = 0;
                foreach (var item in create.Items)
                {
                    var product = productRepository.GetProduct(item.ProductID);
                    if (theaterProductRepository.CheckBuyProduct(create.TheaterID, item.ProductID, item.Amount))
                        totalPrice = item.Amount * product.Price;
                    else
                        cantBuy.Add(item.ProductID);
                }

                Order order = new()
                {
                    ID = Guid.NewGuid(),
                    UserID = userID,
                    PurchasedTime = DateTime.Now,
                    IsPurchased = false,
                    TotalPrice = totalPrice
                };

                foreach (var item in create.Items)
                {
                    var productID = productRepository.GetProduct(item.ProductID).ID;
                    if (theaterProductRepository.CheckBuyProduct(create.TheaterID, item.ProductID, item.Amount))
                    {
                        OrderProduct orderProduct = new()
                        {
                            OrderID = order.ID,
                            ProductID = productID,
                            Amount = item.Amount
                        };
                        orderProductsRepository.AddOrderProduct(orderProduct);
                    }
                }

                if (cantBuy.Count > 0)
                    return Ok(cantBuy);
                return Ok(new JsonResponse() { State = "Success" });
            }

            return BadRequest(new JsonResponse() { State = "Fail" });
        }
    }
}
