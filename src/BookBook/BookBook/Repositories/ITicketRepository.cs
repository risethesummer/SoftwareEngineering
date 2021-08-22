using BookBook.Models;
using System;
using System.Collections.Generic;

namespace BookBook.Repositories
{
    public interface ITicketRepository
    {
        bool AddTicket(Ticket ticket);
        Ticket GetTicket(Guid id);
        IEnumerable<Ticket> GetTickets();
    }
}
