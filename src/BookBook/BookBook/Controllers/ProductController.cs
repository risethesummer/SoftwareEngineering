using BookBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookBook.Models;
using System;
using BookBook.Dtos.Product;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace BookBook.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ITheaterProductRepository theaterProductRepository;
        private readonly ITicketRepository ticketRepository;

        public ProductController(IProductRepository productRepository,
            ITicketRepository ticketRepository, ITheaterProductRepository theaterProductRepository)
        {
            this.productRepository = productRepository;
            this.ticketRepository = ticketRepository;
            this.theaterProductRepository = theaterProductRepository;
        }

        [HttpPost]
        public ActionResult CreateProduct(CreateProductDto create)
        {
            try
            {
                var product = new Product()
                {
                    ID = Guid.NewGuid(),
                    Name = create.Name,
                    Price = create.Price,
                    Type = create.Type
                };
                productRepository.AddProduct(product);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("ticket")]
        public IEnumerable<TicketDto> GetTickets()
        {
            foreach (var tick in ticketRepository.GetTickets())
            {
                var prod = productRepository.GetProduct(tick.ProductID);
                var theaters = theaterProductRepository.GetTheaters(tick.ProductID);
                if (theaters.Any())
                    yield return tick.AsDto(prod, theaters.Select(t => new Dtos.Theater.TheaterDto()
                    {
                        ID = t.ID,
                        Name = t.Name,
                        Location = t.Location
                    })
                    );
            }
        }

        [HttpGet("ticket/{movieID}")]
        public IEnumerable<TicketDto> GetTickets(Guid movieID)
        {
            foreach (var tick in ticketRepository.GetTickets(movieID))
            {
                var prod = productRepository.GetProduct(tick.ProductID);
                var theaters = theaterProductRepository.GetTheaters(tick.ProductID);
                if (theaters.Any())
                    yield return tick.AsDto(prod, theaters.Select(t => new Dtos.Theater.TheaterDto()
                    {
                        ID = t.ID,
                        Name = t.Name,
                        Location = t.Location
                    })
                    );
            }
        }

        [HttpPut]
        public ActionResult UpdateTheaterProduct(UpdateTheaterProductDto update)
        {
            try
            {
                var find = theaterProductRepository.GetTheaterProducts(update.TheaterID, update.ProductID);
                if (find != null)
                {
                    find.Remains = update.Amount;
                    theaterProductRepository.UpdateTheaterProducts(find);
                }
                else
                {
                    var newTP = new TheaterProducts()
                    {
                        ProductID = update.ProductID,
                        TheaterID = update.TheaterID,
                        Remains = update.Amount
                    };
                    theaterProductRepository.AddTheaterProduct(newTP);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("ticket")]
        public ActionResult CreateTicket(CreateTicketDto create)
        {
            try
            {
                var product = new Product()
                {
                    ID = Guid.NewGuid(),
                    Name = create.Name,
                    Price = create.Price,
                    Type = create.Type,
                };
                productRepository.AddProduct(product);

                var ticket = new Ticket()
                {
                    ProductID = product.ID,
                    MovieID = create.MovieID,
                    ShowTime = create.ShowTime,
                    Seat = create.Seat
                };
                ticketRepository.AddTicket(ticket);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
