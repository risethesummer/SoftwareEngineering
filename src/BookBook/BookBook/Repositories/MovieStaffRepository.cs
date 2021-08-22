using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookBook.Repositories
{
    public class MovieStaffRepository : IMovieStaffRepository
    {
        private readonly Data.ApplicationDbContext dbContext;

        public MovieStaffRepository(Data.ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddMovieStaff(MovieStaff movieStaff)
        {
            try
            {
                this.dbContext.MovieStaff.Add(movieStaff);
                this.dbContext.SaveChanges();
            }
            catch
            {

            }
        }

        public void DeleteMovieStaff(Guid movieID, Guid personID)
        {
            try
            {
                var find = this.dbContext.MovieStaff.Find(movieID, personID);
                if (find != null)
                {
                    this.dbContext.MovieStaff.Remove(find);
                    this.dbContext.SaveChanges();
                }
            }
            catch
            {

            }
        }

        public Person GetAStaffOfMovie(Guid movieID, string role)
        {
            var find = dbContext.MovieStaff.FirstOrDefault(s => s.MovieID == movieID && s.Role == role);
            if (find != null)
                return find.Person;
            return null;
        }

        public IEnumerable<Person> GetStaffOfMovie(Guid movieID, string role)
        {
            foreach (var r in dbContext.MovieStaff.Where(r => r.MovieID == movieID && r.Role == role).ToArray())
                yield return dbContext.People.Find(r.PersonID);
        }
    }
}
