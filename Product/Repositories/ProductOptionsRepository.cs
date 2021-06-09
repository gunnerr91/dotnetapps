using ProductApi.Models;
using ProductApi.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductApi.Repositories
{
    public class ProductOptionsRepository : IProductOptionsRepository
    {
        private ProductContext _context;

        public ProductOptionsRepository(ProductContext context)
        {
            _context = context;
        }

        public void AddProductOption(ProductOption productOption)
        {
            _context.ProductOptions.Add(productOption);
            _context.SaveChanges();
        }

        public void DeleteProductOption(Guid productId, Guid optionId)
        {
            var productOptionToRemove = _context.ProductOptions.FirstOrDefault(productOption => productOption.ProductId == productId && productOption.Id == optionId);

            if (productOptionToRemove != null)
            {
                _context.ProductOptions.Remove(productOptionToRemove);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ProductOption> GetAllProductOptions(Guid productId)
        {
            return _context.ProductOptions.Where(option => option.ProductId == productId).ToList();
        }

        public ProductOption GetProductOptionById(Guid productId, Guid optionId)
        {
            return _context.ProductOptions.FirstOrDefault(productOption => productOption.Id == optionId && productOption.ProductId == productId);
        }

        public void UpdateProductOption(Guid optionId, ProductOption productOption)
        {
            var productOptionToUpdate = _context.ProductOptions.FirstOrDefault(productOptionFromDb => productOptionFromDb.Id == optionId && productOptionFromDb.ProductId == productOption.ProductId);

            if (productOptionToUpdate != null)
            {
                productOptionToUpdate.Name = productOption.Name;
                productOptionToUpdate.Description = productOption.Description;
                _context.ProductOptions.Update(productOptionToUpdate);
                _context.SaveChanges();
            }
        }
    }
}
