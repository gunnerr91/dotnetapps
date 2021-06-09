using ProductApi.Models.Interfaces;
using System;

namespace ProductApi.Models
{
    public class ProductOptionsMapper : IProductOptionsMapper
    {
        public ProductOption Map(Guid productId, ProductOptionsDto productOptionsDto)
        {
            return new ProductOption
            {
                Description = productOptionsDto.Description,
                Name = productOptionsDto.Name,
                ProductId = productId
            };
        }
    }
}
