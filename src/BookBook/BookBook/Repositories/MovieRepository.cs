using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookBook.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateMovie(Movie movie)
        {
            try
            {
                if (movie != null)
                {
                    dbContext.Movies.Add(movie);
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

        public bool DeleteMovie(Guid id)
        {
            try
            {
                var find = dbContext.Movies.Find(id);
                dbContext.Movies.Remove(find);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Movie GetMovie(Guid id)
        {
            return dbContext.Movies.Find(id);
        }

        public IEnumerable<Movie> GetMovieByName(string name)
        {
            return dbContext.Movies.Where(m => m.Name.Contains(name)).ToArray();
        }

        public IEnumerable<Movie> GetMovies()
        {
            return dbContext.Movies.ToArray();
        }

        public IEnumerable<Movie> GetTopMovies(int amount)
        {
            return dbContext.Movies.Take(amount);
        }

        public bool UpdateMovie(Movie update)
        {
            try
            {
                var find = dbContext.Movies.Find(update.ID);

                if (find != null && update != null)
                {
                    find.Name = update.Name;
                    find.Description = update.Description;
                    find.Duration = update.Duration;
                    find.Genre = update.Genre;
                    find.ImageID = update.ImageID;
                    find.IMDBStar = update.IMDBStar;
                    find.Nation = update.Nation;
                    find.RequiredAge = update.RequiredAge;
                    find.Year = update.Year;
                    find.YoutubeLink = update.YoutubeLink;
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
