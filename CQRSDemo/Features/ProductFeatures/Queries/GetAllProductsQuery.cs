using CQRSDemo.Context;
using CQRSDemo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSDemo.Features.ProductFeatures.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
        {
            private readonly IApplicationContext context;

            public GetAllProductsQueryHandler(IApplicationContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var productList = await context.Products.ToListAsync();
                if(productList == null)
                {
                    return null;
                }
                return productList.AsReadOnly();
            }
        }
    }
}
