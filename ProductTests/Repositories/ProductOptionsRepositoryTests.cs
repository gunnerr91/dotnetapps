using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductApi.Models;
using ProductApi.Repositories;
using System;
using System.Linq;

namespace ProductTests.Repositories
{
    [TestClass]
    public class ProductOptionsRepositoryTests
    {
        private ProductOptionsRepository _productOptionsRepository;
        private ProductContext _context;
        private DbContextOptions<ProductContext> _options;
        private Guid _productId = Guid.Parse("3EA6A4A7-2745-46BB-8561-7CB1AD64FEA2");
        private Guid _optionId = Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA2");

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ProductContext>().UseInMemoryDatabase(databaseName: "ProductOptionsDb").Options;

            _context = new ProductContext(_options);
            _context.ProductOptions.AddRange(
                new ProductOption
                {
                    Id = Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA1"),
                    ProductId = Guid.Parse("3EA6A4A7-2745-46BB-8561-7CB1AD64FEA1"),
                    Description = "product 1",
                    Name = "product 1"
                },
                new ProductOption
                {
                    Id = _optionId,
                    ProductId = _productId,
                    Description = "product 2 option 1",
                    Name = "product 2"
                },
                new ProductOption
                {
                    Id = Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA3"),
                    ProductId = _productId,
                    Description = "product 2 option 2",
                    Name = "product 2"
                });
            _context.SaveChanges();

            _productOptionsRepository = new ProductOptionsRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllProductOptions_Returns_All_ProductOptions()
        {
            var productOptions = _productOptionsRepository.GetAllProductOptions(_productId).ToList();

            Assert.AreEqual(2, productOptions.Count);
        }

        [TestMethod]
        public void GetAllProductOptions_Returns_Specified_ProductOption()
        {
            var productOption = _productOptionsRepository.GetProductOptionById(_productId, _optionId);
            
            var correctOption = _context.ProductOptions.First(x => x.Id == _optionId);
            Assert.AreEqual(productOption.Name, correctOption.Name);
            Assert.AreEqual(productOption.Id, correctOption.Id);
            Assert.AreEqual(productOption.ProductId, correctOption.ProductId);
            Assert.AreEqual(productOption.Description, correctOption.Description);
        }

        [TestMethod]
        public void AddProductOption_Adds_A_New_ProductOption()
        {
            var newProductOption = new ProductOption
            {
                ProductId = _productId,
                Description = "product 2 option 3 description",
                Name = "product 2 option 3"
            };

            _productOptionsRepository.AddProductOption(newProductOption);

            var productOptionFromContext = _context.ProductOptions.FirstOrDefault(productOption => productOption.Id == newProductOption.Id);
            Assert.AreEqual(newProductOption.Name, productOptionFromContext.Name);
            Assert.AreEqual(newProductOption.ProductId, productOptionFromContext.ProductId);
            Assert.IsNotNull(productOptionFromContext.Id);
            Assert.AreEqual(newProductOption.Description, productOptionFromContext.Description);
        }

        [TestMethod]
        public void UpdateProductOption_Updates_Existing_ProductOption()
        {
            var updatedProductOption = new ProductOption
            {
                ProductId = _productId,
                Description = "new product option desc",
                Name = "new product option name"
            };

            _productOptionsRepository.UpdateProductOption(_optionId, updatedProductOption);

            var updatedProductFromContext = _context.ProductOptions.FirstOrDefault(productOption => productOption.Id == _optionId);
            Assert.AreEqual(updatedProductOption.Name, updatedProductFromContext.Name);
            Assert.AreEqual(_optionId, updatedProductFromContext.Id);
            Assert.AreEqual(updatedProductOption.Description, updatedProductFromContext.Description);
        }

        [TestMethod]
        public void DeleteProductOption_Deletes_Specified_ProductOption()
        {
            Assert.IsTrue(_context.ProductOptions.ToList().Count == 3);
            _productOptionsRepository.DeleteProductOption(_productId, _optionId);

            Assert.IsNull(_context.ProductOptions.FirstOrDefault(productOption => productOption.Id == _optionId && productOption.ProductId == _productId));
            Assert.IsTrue(_context.ProductOptions.ToList().Count == 2);
        }

    }
}
