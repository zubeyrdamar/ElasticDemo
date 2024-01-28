using Elastic.API.Models;
using Nest;

namespace Elastic.API.Repository
{
    public class ProductRepository
    {
        private readonly ElasticClient _client;

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product newProduct)
        {
            newProduct.CreatedAt = DateTime.Now;

            var response = await _client.IndexAsync(newProduct, i => i.Index("products"));

            if (!response.IsValid)
            {
                return null;
            }

            newProduct.Id = response.Id;
            return newProduct;
        }
    }
}
