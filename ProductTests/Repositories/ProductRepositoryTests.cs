using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductApi.Models;
using ProductApi.Repositories;
using System;
using System.Linq;

namespace ProductTests.Repositories
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private ProductRepository _productRepository;
        private ProductContext _context;
        private DbContextOptions<ProductContext> _options;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ProductContext>().UseInMemoryDatabase(databaseName: "ProductDb").Options;

            _context = new ProductContext(_options);
            _context.Products.AddRange(
                new Product
                {
                    Id = Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA1"),
                    Description = "product 1",
                    Name = "product 1"
                },
                new Product
                {
                    Id = Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA2"),
                    Description = "product 2",
                    Name = "product 2"
                });
            _context.SaveChanges();

            _productRepository = new ProductRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllProducts_Returns_All_Products()
        {
            var products = _productRepository.GetAllProducts().ToList();

            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public void GetProductByName_Returns_Specified_Product()
        {
            var product = _productRepository.GetProductByName("product 2");

            Assert.AreEqual(product.Name, "product 2");
            Assert.AreEqual(product.Id, Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA2"));
            Assert.AreEqual(product.Description, "product 2");
        }

        [TestMethod]
        public void GetProductById_Returns_Specified_Product()
        {
            var product = _productRepository.GetProductById(Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA2"));

            Assert.AreEqual(product.Name, "product 2");
            Assert.AreEqual(product.Id, Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA2"));
            Assert.AreEqual(product.Description, "product 2");
        }

        [TestMethod]
        public void AddProduct_Adds_A_New_Product()
        {
            var newProduct = new Product
            {
                Id = Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA6"),
                Description = "product 3",
                Name = "product 3"
            };

            _productRepository.AddProduct(newProduct);

            var productFromContext = _context.Products.FirstOrDefault(product => product.Id == newProduct.Id);
            Assert.AreEqual(newProduct.Name, productFromContext.Name);
            Assert.IsNotNull(productFromContext.Id);
            Assert.AreEqual(newProduct.Description, productFromContext.Description);
        }

        [TestMethod]
        public void UpdateProduct_Updates_Existing_Product()
        {
            var productId = Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA2");
            var updatedProduct = new Product
            {
                Description = "new product desc",
                Name = "new product name",
                DeliveryPrice = 1.1M,
                Price = 1.0M
            };

            _productRepository.UpdateProduct(productId, updatedProduct);

            var updatedProductFromContext = _context.Products.FirstOrDefault(product => product.Id == productId);
            Assert.AreEqual(updatedProduct.Name, updatedProductFromContext.Name);
            Assert.AreEqual(productId, updatedProductFromContext.Id);
            Assert.AreEqual(updatedProduct.Description, updatedProductFromContext.Description);
            Assert.AreEqual(updatedProduct.DeliveryPrice, updatedProductFromContext.DeliveryPrice);
            Assert.AreEqual(updatedProduct.Price, updatedProductFromContext.Price);
        }

        [TestMethod]
        public void DeleteProduct_Deletes_Specified_Product()
        {
            _productRepository.DeleteProduct(Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA2"));

            Assert.IsNull(_context.Products.FirstOrDefault(product => product.Id == Guid.Parse("2EA6A4A7-2745-46BB-8561-7CB1AD64FEA2")));
            Assert.IsTrue(_context.Products.ToList().Count == 1);
        }

    }
}
