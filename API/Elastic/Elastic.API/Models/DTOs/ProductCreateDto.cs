﻿namespace Elastic.API.Models.DTOs
{
    public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto Feature)
    {
        public Product CreateProduct()
        {
            return new Product {
                Name = Name, 
                Price = Price, 
                Stock = Stock, 
                Feature = new ProductFeature()
                {
                    Width = Feature.Width,
                    Height = Feature.Height,
                    Color = Feature.Color,
                }
            };
        }
    }
}
