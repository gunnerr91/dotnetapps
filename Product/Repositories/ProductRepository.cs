using ProductApi.Models;
using ProductApi.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public void AddProduct(Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        public void DeleteProduct(Guid id)
        {
            var productToRemove = _context.Products.FirstOrDefault(product => product.Id == id);

            if (productToRemove != null)
            {
                _context.Products.Remove(productToRemove);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public Product GetProductById(Guid id)
        {
            return _context.Products.FirstOrDefault(product => product.Id == id);
        }

        public Product GetProductByName(string name)
        {
            return _context.Products.FirstOrDefault(product => product.Name == name);
        }

        public void UpdateProduct(Guid productId, Product product)
        {
            var productToUpdate = _context.Products.FirstOrDefault(productFromDb => productFromDb.Id == productId);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Description = product.Description;
                productToUpdate.DeliveryPrice = product.DeliveryPrice;

                _context.Products.Update(productToUpdate);
                _context.SaveChanges();
            }
        }
    }
}
