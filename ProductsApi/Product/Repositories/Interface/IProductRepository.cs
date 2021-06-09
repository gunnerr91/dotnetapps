using ProductApi.Models;
using System;
using System.Collections.Generic;

namespace ProductApi.Repositories.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductByName(string name);
        Product GetProductById(Guid id);
        void AddProduct(Product newProduct);
        void UpdateProduct(Guid productId, Product product);
        void DeleteProduct(Guid id);
    }
}