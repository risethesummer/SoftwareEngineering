﻿using System;

namespace BookBook.Dtos.Product
{
    public record CreateTicketDto : CreateProductDto
    {
        public Guid MovieID { get; init; }

        public DateTime ShowTime { get; init; }
    }
}
