using BookBook.Models;
using System;
using System.Collections.Generic;

namespace BookBook.Repositories
{
    public interface IMovieStaffRepository
    {
        void AddMovieStaff(MovieStaff movieStaff);
        void DeleteMovieStaff(Guid movieID, Guid personID);
        IEnumerable<Person> GetStaffOfMovie(Guid movieID, string role);
        Person GetAStaffOfMovie(Guid movieID, string role);
    }
}
