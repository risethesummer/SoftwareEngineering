using System;

namespace BookBook.Dtos
{
    namespace Account
    {
        public record UserSessionDto
        {
            public Guid SessionID { get; init; }
            public string Account { get; init; }
            public string Name { get; init; }
            public string Email { get; init; }
            public string DayOfBirth { get; init; }
            public string Address { get; init; }
        }
    }

}