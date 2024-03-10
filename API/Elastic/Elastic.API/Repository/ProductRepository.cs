﻿using Elastic.API.Models;
using Nest;
using System.Collections.Immutable;

namespace Elastic.API.Repository
{
    public class ProductRepository
    {
        private readonly ElasticClient _client;
        private const string indexName = "products";

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product newProduct)
        {
            newProduct.CreatedAt = DateTime.Now;

            var response = await _client.IndexAsync(newProduct, i => i.Index(indexName).Id(Guid.NewGuid().ToString()));

            if (!response.IsValid)
            {
                return null;
            }

            newProduct.Id = response.Id;
            return newProduct;
        }

        public async Task<ImmutableList<Product>> GetAllAsync()
        {
            var result = await _client.SearchAsync<Product>(s => s.Index(indexName).Query(q => q.MatchAll()));

            foreach (var hit in result.Hits)
            {
                hit.Source.Id = hit.Id;
            }

            return result.Documents.ToImmutableList();
        }
    }
}
