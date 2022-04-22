using System;

namespace BookBook.Dtos
{
    namespace Account
    {
        public record ResetPasswordConfirmDto
        {
            public int MailCode { get; init; }
        }
    }
}