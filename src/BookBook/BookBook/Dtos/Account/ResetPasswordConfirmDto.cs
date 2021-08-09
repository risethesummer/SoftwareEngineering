using System;

namespace BookBook.Dtos
{
    namespace Account
    {
        public record ResetPasswordConfirmDto
        {
            public Guid ID { get; init; }
            public string MailCode { get; init; }
        }
    }
}