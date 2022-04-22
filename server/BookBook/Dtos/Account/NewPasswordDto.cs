using System;

namespace BookBook.Dtos
{
    namespace Account
    {
        public record NewPasswordDto
        {
            public Guid ID { get; init; }
            public string NewPassword { get; init; }
        }
    }
}