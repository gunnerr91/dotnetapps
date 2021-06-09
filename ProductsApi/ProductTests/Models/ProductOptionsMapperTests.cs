using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductApi.Models;
using System;

namespace ProductTests.Models
{
    [TestClass]
    public class ProductOptionsMapperTests
    {
        [TestMethod]
        public void Map_Returns_Product_Options_Model()
        {
            var productId = Guid.Parse("DA8CEAF2-3D67-4DF5-83DB-ED7F213D8F90");
            var dtoModel = new ProductOptionsDto
            {
                Name = "Product 1",
                Description = "This is a product"
            };
            var mapper = new ProductOptionsMapper();

            var result = mapper.Map(productId, dtoModel);

            Assert.IsNotNull(result.Id);
            Assert.AreEqual(productId, result.ProductId);
            Assert.AreEqual(dtoModel.Name, result.Name);
            Assert.AreEqual(dtoModel.Description, result.Description);
        }
    }
}
