using Microsoft.AspNetCore.Http;

namespace BookBook.Dtos.Product
{
    public record CreateProductDto
    {
        public string Name { get; init; }
        public string Type { get; init; }
        public int Price { get; init; }
    }
}
