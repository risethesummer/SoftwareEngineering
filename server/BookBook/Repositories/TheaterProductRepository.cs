using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public class TheaterProductRepository : ITheaterProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TheaterProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddTheaterProduct(TheaterProducts theaterProducts)
        {
            try
            {
                dbContext.TheaterProducts.Add(theaterProducts);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool BuyProduct(Guid theaterID, Guid pID, int amount)
        {
            try
            {
                var find = dbContext.TheaterProducts.Find(theaterID, pID);
                if (find != null && find.Remains - amount >= 0)
                {
                    find.Remains -= amount;
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckBuyProduct(Guid theaterID, Guid pID, int amount)
        {
            try
            {
                var find = dbContext.TheaterProducts.Find(theaterID, pID);
                if (find != null && find.Remains - amount >= 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteTheaterProduct(Guid theaterID, Guid pID)
        {
            try
            {
                var find = dbContext.TheaterProducts.Find(theaterID, pID);
                if (find != null)
                {
                    dbContext.TheaterProducts.Remove(find);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public TheaterProducts GetTheaterProducts(Guid theaterId, Guid pid)
        {
            return dbContext.TheaterProducts.Find(theaterId, pid);
        }

        public IEnumerable<Theater> GetTheaters(Guid pid)
        {
            foreach (var tp in dbContext.TheaterProducts.Where(t => t.ProductID == pid && t.Remains > 0).ToArray())
            {
                yield return dbContext.Theaters.Find(tp.TheaterID);
            }
        }

        public bool UpdateTheaterProducts(TheaterProducts update)
        {
            try
            {
                var find = dbContext.TheaterProducts.Find(update.TheaterID, update.ProductID);
                find.Remains = update.Remains;
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
