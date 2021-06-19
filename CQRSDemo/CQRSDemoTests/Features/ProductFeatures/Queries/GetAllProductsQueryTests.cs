using CQRSDemo.Context;
using CQRSDemo.Features.ProductFeatures.Queries;
using CQRSDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CQRSDemo.Features.ProductFeatures.Queries.GetAllProductsQuery;

namespace CQRSDemoTests.Features.ProductFeatures.Queries
{
    public class GetAllProductsQueryTests : TestHelper
    {
        [Fact]
        public async Task Retrieves_All_Products()
        {
            appDbContext.Products.AddRange(new Product(), new Product());
            await appDbContext.SaveChanges();
            var requestQuery = new GetAllProductsQueryHandler(appDbContext);
            var result = await requestQuery.Handle(new GetAllProductsQuery(), new System.Threading.CancellationToken());
            Assert.Equal(2, result.Count());
        }
    }
}
