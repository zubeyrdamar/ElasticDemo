using Elastic.API.Models.DTOs;
using Elastic.API.Repository;

namespace Elastic.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
            // Normally we would create a new product here and convert this product to productDto
            // Instead, we go into ProductCreateDto and handle mapping in that record (Best practice)

            var response = await _repository.SaveAsync(request.CreateProduct());

            if (response == null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> { "Unexpected error occured while saving a record." }, System.Net.HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(response.CreateDto(), System.Net.HttpStatusCode.Created);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            var productListDto = new List<ProductDto>();

            foreach(var product in products)
            {
                if(product.Feature is null)
                {
                    productListDto.Add(new ProductDto(product.Id, product.Name, product.Price, product.Stock, null));
                }
                else
                {
                    productListDto.Add(new ProductDto(product.Id, product.Name, product.Price, product.Stock, new ProductFeatureDto(product.Feature.Width, product.Feature.Height, product.Feature.Color)));
                }
            }

            // var productListDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock, new ProductFeatureDto(p.Feature.Width, p.Feature.Height, p.Feature.Color))).ToList();

            return ResponseDto<List<ProductDto>>.Success(productListDto, System.Net.HttpStatusCode.OK);
        }
    }
}
