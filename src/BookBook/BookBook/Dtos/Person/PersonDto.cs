using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Dtos.Person
{
    public record PersonDto : CompactPersonDto
    {
        public string DayOfBirth { get; init; }
        public Guid ImageID { get; init; }
        public string Nation { get; init; }
        public string Description { get; init; }
    }
}
