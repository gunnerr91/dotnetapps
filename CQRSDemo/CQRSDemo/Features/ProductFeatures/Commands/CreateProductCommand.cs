using CQRSDemo.Context;
using CQRSDemo.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSDemo.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal Rate { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationContext context;

            public CreateProductCommandHandler(IApplicationContext context)
            {
                this.context = context;
            }
            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product 
                {
                    Barcode = command.Barcode,
                    BuyingPrice = command.BuyingPrice,
                    Name = command.Name,
                    Rate = command.Rate,
                    Description = command.Description
                };

                context.Products.Add(product);
                await context.SaveChanges();

                return product.Id;
            }
        }
    }
}
