using ProductApi.Models;
using System;
using System.Collections.Generic;

namespace ProductApi.Repositories.Interface
{
    public interface IProductOptionsRepository
    {
        IEnumerable<ProductOption> GetAllProductOptions(Guid productId);
        ProductOption GetProductOptionById(Guid productId, Guid optionId);
        void AddProductOption(ProductOption productOption);
        void UpdateProductOption(Guid optionId, ProductOption productOption);
        void DeleteProductOption(Guid productId, Guid optionId);
    }
}