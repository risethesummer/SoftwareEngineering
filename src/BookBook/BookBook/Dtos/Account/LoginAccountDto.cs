
namespace BookBook.Dtos
{
    namespace Account
    {
        public record LoginAccountDto
        {
            public string Account { get; init; }
            public string Password { get; init; }
        }
    }

}