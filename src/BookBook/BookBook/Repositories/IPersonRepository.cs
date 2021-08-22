using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Models;

namespace BookBook.Repositories
{
    public interface IPersonRepository
    {
        bool CreatePerson(Person person);
        bool UpdatePerson(Guid id, Person update);
        Person GetPerson(Guid id);
        IEnumerable<Person> GetPeople();
    }
}
