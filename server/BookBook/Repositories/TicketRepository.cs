using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext dbContext;
        public TicketRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddTicket(Ticket ticket)
        {
            try
            {
                dbContext.Tickets.Add(ticket);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Ticket GetTicket(Guid id)
        {
            return dbContext.Tickets.Find(id);
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return dbContext.Tickets.AsEnumerable().ToArray();
        }

        public IEnumerable<Ticket> GetTickets(Guid movieID)
        {
            return dbContext.Tickets.Where(a => a.MovieID == movieID).ToArray();
        }
    }
}
