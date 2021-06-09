using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductApi.Models;

namespace ProductTests.Models
{
    [TestClass]
    public class ProductMapperTests
    {
        [TestMethod]
        public void Map_Returns_Product_Model()
        {
            var dtoModel = new ProductDto
            {
                Name = "Product 1",
                Description = "This is a product",
                Price = 1.1M,
                DeliveryPrice = 1.1M
            };
            var mapper = new ProductMapper();

            var result = mapper.Map(dtoModel);

            Assert.IsNotNull(result.Id);
            Assert.AreEqual(dtoModel.Name, result.Name);
            Assert.AreEqual(dtoModel.Price, result.Price);
            Assert.AreEqual(dtoModel.DeliveryPrice, result.DeliveryPrice);
            Assert.AreEqual(dtoModel.Description, result.Description);
        }

    }
}
