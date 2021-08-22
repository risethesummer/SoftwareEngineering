using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public interface ITheaterRepository
    {
        void AddTheater(Theater theater);
        void UpdateTheater(Theater update);
        Theater GetTheater(Guid id);
        IEnumerable<Theater> GetTheaters();
        IEnumerable<Theater> GetTheaterByName(string name);
        IEnumerable<Theater> GetTheaterByLocation(string location);
    }
}
