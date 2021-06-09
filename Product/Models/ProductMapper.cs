using ProductApi.Models.Interfaces;

namespace ProductApi.Models
{
    public class ProductMapper : IProductMapper
    {
        public Product Map(ProductDto productDto)
        {
            return new Product
            {
                DeliveryPrice = productDto.DeliveryPrice,
                Description = productDto.Description,
                Name = productDto.Name,
                Price = productDto.Price
            };
        }
    }
}
