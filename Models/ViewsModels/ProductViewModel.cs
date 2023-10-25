﻿namespace WebApiMongoDB.Models.ViewsModels
{
    public class ProductViewModel
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidityDate { get; set; }
    }
}