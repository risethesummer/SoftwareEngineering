using System;

namespace BookBook.Dtos
{
    namespace Account
    {
        public record ResetPasswordDto
        {
            public string Account { get; init; }
            public string Email { get; init; }
        }
    }
}