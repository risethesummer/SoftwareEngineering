using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookBook.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext dbContext;

        public PersonRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreatePerson(Person person)
        {
            try
            {
                dbContext.People.Add(person);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
           
        }

        public IEnumerable<Person> GetPeople()
        {
            return dbContext.People.AsEnumerable();
        }

        public Person GetPerson(Guid id)
        {
            return dbContext.People.Find(id);
        }

        public bool UpdatePerson(Guid id, Person update)
        {
            try
            {
                var find = dbContext.People.Find(id);
                
                if (find != null && update != null)
                {
                    find.Name = update.Name;
                    find.Nation = update.Nation;
                    find.DayOfBirth = update.DayOfBirth;
                    find.Description = update.Description;
                    find.ImageID = update.ImageID;
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
    }
}
