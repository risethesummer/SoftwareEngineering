using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly Data.ApplicationDbContext dbContext;

        public TheaterRepository(Data.ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddTheater(Theater theater)
        {
            try
            {
                dbContext.Theaters.Add(theater);
                dbContext.SaveChanges();
            }
            catch
            { 
            }
        }

        public Theater GetTheater(Guid id)
        {
            return dbContext.Theaters.Find(id);
        }

        public IEnumerable<Theater> GetTheaterByLocation(string location)
        {
            return dbContext.Theaters.Where(t => t.Location.Contains(location));
        }
        public IEnumerable<Theater> GetTheaterByName(string name)
        {
            return dbContext.Theaters.Where(t => t.Name.Contains(name));
        }

        public IEnumerable<Theater> GetTheaters()
        {
            return dbContext.Theaters.AsEnumerable();
        }

        public void UpdateTheater(Theater update)
        {
            try
            {
                if (update != null)
                {
                    var find = dbContext.Theaters.Find(update.ID);
                    if (find != null)
                    {
                        find.Name = update.Name;
                        find.Location = update.Location;
                        dbContext.SaveChanges();
                    }
                }
            }
            catch
            {

            }
        }
    }
}
