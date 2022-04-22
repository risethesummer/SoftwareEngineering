using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Dtos.Person
{
    public class CreatePersonDto
    {
        public string Name { get; init; }
        public DateTime DayOfBirth { get; init; }
        public Guid ImageID { get; init; }
        public string Nation { get; init; }
        public string Description { get; init; }
    }
}
