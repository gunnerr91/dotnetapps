using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Models.Interfaces;
using ProductApi.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ProductTests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        private List<Product> _products;
        private IProductRepository _productRepository;
        private IProductOptionsRepository _productOptionsRepository;
        private ProductsController _controller;
        private IProductMapper _productMapper;
        private IProductOptionsMapper _productOptionsMapper;
        private Guid _productId = Guid.Parse("A08AF536-737C-4C10-B330-F4C91C2DE1E4");
        private Guid _optionId = Guid.Parse("A08AF536-737C-4C10-B330-F4C91C2DE1E6");

        [TestInitialize]
        public void Setup()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse("640DBD11-A2BC-466A-A9DD-7248B725C3ED"),
                    Name = "Product 1"
                },
                new Product
                {
                    Id = Guid.Parse("640DBD11-A2BC-466A-A9DD-7248B725C3EE"),
                    Name = "Product 2"
                }
            };
            _productRepository = Substitute.For<IProductRepository>();
            _productOptionsRepository = Substitute.For<IProductOptionsRepository>();
            _productMapper = Substitute.For<IProductMapper>();
            _productOptionsMapper = Substitute.For<IProductOptionsMapper>();
            _controller = new ProductsController(_productRepository, _productMapper, _productOptionsRepository, _productOptionsMapper);
        }

        [TestMethod]
        public void Get_Returns_All_Products()
        {
            _productRepository.GetAllProducts().Returns(_products);

            var result = _controller.Get() as ObjectResult;

            var productsFromRequest = result.Value as List<Product>;
            Assert.AreEqual(HttpStatusCode.OK.GetHashCode(), result.StatusCode.Value);
            Assert.AreEqual(_products.Count, productsFromRequest.Count);
            _productRepository.Received(1).GetAllProducts();
        }

        [TestMethod]
        public void Get_Returns_Product_By_Name()
        {
            var product = _products.Last();
            _productRepository.GetProductByName(product.Name).Returns(product);

            var result = _controller.Get(product.Name) as ObjectResult;

            var productFromRequest = result.Value as Product;
            Assert.AreEqual(HttpStatusCode.OK.GetHashCode(), result.StatusCode.Value);
            Assert.AreEqual(product.Id, productFromRequest.Id);
        }

        [TestMethod]
        public void Get_Returns_Not_Found_When_Product_Is_Not_Found()
        {
            var result = _controller.Get("product 47");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Get_Returns_Product_By_Id()
        {
            var product = _products.Last();
            _productRepository.GetProductById(product.Id).Returns(product);

            var result = _controller.Get(product.Id) as ObjectResult;

            var productFromRequest = result.Value as Product;
            Assert.AreEqual(HttpStatusCode.OK.GetHashCode(), result.StatusCode.Value);
            Assert.AreEqual(product.Id, productFromRequest.Id);
        }

        [TestMethod]
        public void Get_Returns_Not_Found_When_Product_Is_Not_Found_By_Id()
        {
            var result = _controller.Get(Guid.Parse("DFA5F18B-EDC6-4F50-9D56-4B3FD9CB901A")) as NotFoundResult;

            Assert.AreEqual(HttpStatusCode.NotFound.GetHashCode(), result.StatusCode);
        }

        [TestMethod]
        public void Post_Adds_New_Product()
        {
            var productDto = new ProductDto();
            var newProduct = new Product();
            _productMapper.Map(productDto).Returns(newProduct);

            var result = _controller.Post(productDto) as OkResult;

            Assert.AreEqual(HttpStatusCode.OK.GetHashCode(), result.StatusCode);
            _productRepository.Received(1).AddProduct(newProduct);
        }

        [TestMethod]
        public void Put_Updates_Specified_Product()
        {
            var updatedProduct = new Product
            {
                Id = _products.Last().Id,
                Name = "New name"
            };
            _productRepository.GetProductById(updatedProduct.Id).Returns(_products.Last());
            var productId = _products.Last().Id;
            var mapperDto = new ProductDto();
            _productMapper.Map(mapperDto).Returns(updatedProduct);

            var result = _controller.Put(productId, mapperDto) as OkResult;

            Assert.AreEqual(HttpStatusCode.OK.GetHashCode(), result.StatusCode);
            _productRepository.Received(1).UpdateProduct(productId, updatedProduct);
        }

        [TestMethod]
        public void Put_Returns_Not_Found_For_Invalid_Id()
        {
            var result = _controller.Put(Guid.NewGuid(), new ProductDto()) as NotFoundResult;

            Assert.AreEqual(HttpStatusCode.NotFound.GetHashCode(), result.StatusCode);
        }

        [TestMethod]
        public void Delete_Removed_Specified_Product()
        {
            var productIdToRemove = _products.Last().Id;
            _productRepository.GetProductById(productIdToRemove).Returns(_products.Last());

            var result = _controller.Delete(productIdToRemove) as OkResult;

            Assert.AreEqual(HttpStatusCode.OK.GetHashCode(), result.StatusCode);
            _productRepository.Received(1).DeleteProduct(productIdToRemove);
        }

        [TestMethod]
        public void Delete_Returns_Not_Found_For_Invalid_Id()
        {
            var result = _controller.Delete(Guid.Parse("DFA5F18B-EDC6-4F50-9D56-4B3FD9CB901A")) as NotFoundResult;

            Assert.AreEqual(HttpStatusCode.NotFound.GetHashCode(), result.StatusCode);
        }

        [TestMethod]
        public void GetOptions_Returns_All_Options_Of_Specified_Product_With_Ok_Status()
        {
            var productOptions = new List<ProductOption>
             {
                 new ProductOption(),  new ProductOption()
             };
            _productOptionsRepository.GetAllProductOptions(_productId).Returns(productOptions);

            var result = _controller.GetOptions(_productId) as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actualProductOptions = result.Value as List<ProductOption>;
            Assert.AreEqual(productOptions, actualProductOptions);
        }

        [TestMethod]
        public void GetOptions_Returns_Not_Found_When_Product_Id_Mismatches()
        {
            var result = _controller.GetOptions(_productId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetOptions_Returns_Specified_Option_With_Ok_Result()
        {
            var productOption = new ProductOption();
            _productOptionsRepository.GetProductOptionById(_productId, _optionId).Returns(productOption);

            var result = _controller.GetOptions(_productId, _optionId) as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actualProductOption = result.Value as ProductOption;
            Assert.AreEqual(productOption, actualProductOption);
        }

        [TestMethod]
        public void GetOptions_Returns_Not_Found_When_Specified_Option_Is_Not_Available()
        {
            var result = _controller.GetOptions(_productId, _optionId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostOption_Adds_New_Product_Option_With_Ok_Result()
        {
            var productOptionToBeAdded = new ProductOptionsDto();
            var productOption = new ProductOption();
            _productOptionsMapper.Map(_productId, productOptionToBeAdded).Returns(productOption);

            var result = _controller.PostOption(_productId, productOptionToBeAdded);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            _productOptionsRepository.Received(1).AddProductOption(productOption);
        }

        [TestMethod]
        public void PutOption_Updates_Existing_Option()
        {
            var productOption = new ProductOption
            {
                Id = _optionId,
                ProductId = _productId,
            };
            var productOptionDto = new ProductOptionsDto
            {
                Description = "new desc",
                Name = "new name",
            };
            _productOptionsMapper.Map(_productId, productOptionDto).Returns(productOption);
            _productOptionsRepository.GetProductOptionById(_productId, _optionId).Returns(productOption);

            var result = _controller.PutOption(_productId, _optionId, productOptionDto);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            _productOptionsRepository.Received(1).UpdateProductOption(_optionId, productOption);
        }

        [TestMethod]
        public void PutOption_Returns_Not_Found_When_Option_Is_Not_Available()
        {
            var result = _controller.PutOption(_productId, _optionId, new ProductOptionsDto());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteOption_Deletes_Specified_Option_With_Ok_Result()
        {
            var productOption = new ProductOption
            {
                Id = _optionId,
                ProductId = _productId,
            };
            _productOptionsRepository.GetProductOptionById(_productId, _optionId).Returns(productOption);

            var result = _controller.DeleteOption(_productId, _optionId);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            _productOptionsRepository.Received(1).DeleteProductOption(_productId, _optionId);
        }

        [TestMethod]
        public void DeleteOption_Returns_Not_Found_When_Specified_Option_Is_Not_Available()
        {
            var result = _controller.DeleteOption(Guid.NewGuid(), Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
