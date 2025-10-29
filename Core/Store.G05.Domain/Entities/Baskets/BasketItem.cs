﻿namespace Store.G05.Domain.Entities.Baskets
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}