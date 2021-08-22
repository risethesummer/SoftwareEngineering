using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Dtos.Account;
using BookBook.Dtos.Movies;
using BookBook.Dtos.Order;
using BookBook.Dtos.Person;
using BookBook.Dtos.Product;
using BookBook.Dtos.Theater;
using BookBook.Models;

namespace BookBook
{
    public static class ExtensionMethods
    {
        public static UserSessionDto AsDto (this UserAccount user, Guid sesId)
        {
            return new UserSessionDto()
            { 
                SessionID = sesId,
                Account = user.Account,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                DayOfBirth = user.DayOfBirth
            };
        }

        public static PersonDto AsDto(this Person person)
        {
            return new PersonDto()
            {
                ID = person.ID,
                Name = person.Name,
                DayOfBirth = person.DayOfBirth.ToString(),
                Nation = person.Nation,
                Description = person.Description,
                ImageID = person.ImageID
            };
        }

        public static TicketDto AsDto(this Ticket ticket, Product product, IEnumerable<Guid> theaters)
        {
            return new TicketDto()
            {
                ID = product.ID,
                Name = product.Name,
                MovieID = ticket.MovieID,
                Price = product.Price,
                ShowTime = ticket.ShowTime.ToShortDateString(),
                TheaterID = theaters,
                Type = product.Type
            };
        }

        public static OrderDto AsDto(this Order order, IEnumerable<OrderProduct> pIDs)
        {
            return new OrderDto()
            {
                ID = order.ID,
                PurchasedTime = order.PurchasedTime.ToShortDateString(),
                IsPurchased = order.IsPurchased,
                TotalPrice = order.TotalPrice,
                Items = pIDs.Select(o => new ItemDto()
                {
                    ProductID = o.ProductID,
                    Amount = o.Amount
                })
            };
        }
        public static MovieDto AsDto(this Movie movie, CompactPersonDto director, IEnumerable<CompactPersonDto> actors)
        {
            return new MovieDto()
            {
                ID = movie.ID,
                Actors = actors,
                Description = movie.Description,
                RequiredAge = movie.RequiredAge,
                Director = director,
                Duration = movie.Duration,
                Genre = movie.Genre,
                ImageID = movie.ImageID,
                IMDBStar = movie.IMDBStar,
                Name = movie.Name,
                Nation = movie.Nation,
                Year = movie.Year,
                YoutubeLink = movie.YoutubeLink,
            };
        }

        public static TheaterDto AsDto(this Theater theater)
        {
            return new TheaterDto()
            {
                ID = theater.ID,
                Name = theater.Name,
                Location = theater.Location
            };

        }


        public static CompactPersonDto AsCompactDto (this Person person)
        {
            return new CompactPersonDto()
            {
                ID = person.ID,
                Name = person.Name
            };
        }
    }
}
