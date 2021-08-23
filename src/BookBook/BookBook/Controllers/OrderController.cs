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
        private readonly ITheaterProductRepository theaterProductRepository;
        private readonly IUserActivitiesManager userActivitiesManager;

        public OrderController(IOrderRepository orderRepository,
            ITheaterProductRepository theaterProductRepository,
            IUserActivitiesManager userActivitiesManager)
        {
            this.orderRepository = orderRepository;
            this.theaterProductRepository = theaterProductRepository;
            this.userActivitiesManager = userActivitiesManager;
        }

        [HttpGet]
        public IEnumerable<OrderDto> GetOrders()
        {
            foreach (Order order in orderRepository.GetOrders())
                yield return order.AsDto();
        }

        [HttpPut("{oid}")]
        public ActionResult PurchaseOrder(Guid oid, Guid sessionID)
        {
            if (userActivitiesManager.IsValidSession(sessionID))
            {
                if (orderRepository.PurchaseOrder(oid))
                    return Ok();
            }
            return BadRequest();
        }
        
        [HttpGet("notPurchased/{sessionID}")]
        public IEnumerable<OrderDto> GetNotPurchasedOrders(Guid sessionID)
        {
            if (userActivitiesManager.IsValidSession(sessionID))
            {
                var userID = userActivitiesManager.GetUserId(sessionID);
                foreach (Order order in orderRepository.GetOrdersOfUser(userID, false))
                {
                    yield return order.AsDto();
                }
            }
        }

        [HttpGet("purchased/{sessionID}")]
        public IEnumerable<OrderDto> GetPurchasedOrders(Guid sessionID)
        {
            if (userActivitiesManager.IsValidSession(sessionID))
            {
                var userID = userActivitiesManager.GetUserId(sessionID);
                foreach (Order order in orderRepository.GetOrdersOfUser(userID, true))
                {
                    yield return order.AsDto();
                }
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
                if (theaterProductRepository.CheckBuyProduct(create.TheaterID, create.ProductID, 1))
                {
                    Order order = new()
                    {
                        ID = Guid.NewGuid(),
                        UserID = userID,
                        PurchasedTime = DateTime.Now,
                        IsPurchased = false,
                    };
                    orderRepository.AddOrder(order);
                    return Ok(new JsonResponse() { State = "Success" });
                }
            }
            return BadRequest(new JsonResponse() { State = "Fail" });
        }
    }
}
