using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Dtos.Movies
{
    public record SearchMovieDto
    {
        public Guid SessionID { get; init; }
        public string Name { get; init; }
    }
}
