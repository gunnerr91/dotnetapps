using CQRSDemo.Features.ProductFeatures.Commands;
using CQRSDemo.Models;
using CQRSDemo.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CQRSDemoTests.Validators
{
    public class CreateProductCommandValidatorTests
    {
        [Fact]
        public void Empty_Barcode_Returns_Validation_Error()
        {
            var productModel = new CreateProductCommand();
            var result = new CreateProductCommandValidator().Validate(productModel);
            Assert.Contains(result.Errors, x => x.PropertyName == "Barcode");
        }
    }
}
