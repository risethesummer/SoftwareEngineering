using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Dtos.Movies
{
    public record MovieDto
    {
        public Guid ID { get; init; }

        public string Name { get; init; }

        public int Year { get; init; }

        public string Nation { get; init; }
        public string Genre { get; init; }

        public int RequiredAge { get; init; }
        public int Duration { get; init; }

        public string Description { get; init; }

        public Person.CompactPersonDto Director { get; init; }

        public Guid ImageID { get; init; }

        public IEnumerable<Person.CompactPersonDto> Actors { get; init; }

        public int IMDBStar { get; init; }

        public string YoutubeLink { get; init; }
    }
}
