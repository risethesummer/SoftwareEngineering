using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Repositories
{
    public interface IMovieRepository
    {
        bool CreateMovie(Movie movie);
        bool UpdateMovie(Movie update);
        Movie GetMovie(Guid id);
        bool DeleteMovie(Guid id);
        IEnumerable<Movie> GetMovieByName(string name);
        IEnumerable<Movie> GetMovies();
        IEnumerable<Movie> GetTopMovies(int amount);
    }
}
