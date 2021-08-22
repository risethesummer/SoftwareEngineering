using System;
using System.Collections.Generic;
using System.Linq;
using BookBook.Models;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public interface ITheaterProductRepository
    {
        bool AddTheaterProduct(TheaterProducts theaterProducts);
        bool DeleteTheaterProduct(Guid theaterID, Guid pID);
        bool UpdateTheaterProducts(TheaterProducts update);
        TheaterProducts GetTheaterProducts(Guid theaterId, Guid pid);
        bool CheckBuyProduct(Guid theaterID, Guid pID, int amount);
        bool BuyProduct(Guid theaterID, Guid pID, int amount);
        IEnumerable<Guid> GetTheaters(Guid pid);
    }
}
