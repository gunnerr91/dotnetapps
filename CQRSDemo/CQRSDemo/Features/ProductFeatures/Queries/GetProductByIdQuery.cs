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
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly IApplicationContext context;

            public GetProductByIdQueryHandler(IApplicationContext context)
            {
                this.context = context;
            }

            public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
                if(product == null)
                {
                    return null;
                }
                return product;
            }
        }
    }
}
