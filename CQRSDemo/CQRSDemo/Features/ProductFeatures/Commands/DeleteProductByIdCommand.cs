using CQRSDemo.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSDemo.Features.ProductFeatures.Commands
{
    public class DeleteProductByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, int>
        {
            private readonly IApplicationContext context;

            public DeleteProductByIdCommandHandler(IApplicationContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await context.Products.FirstOrDefaultAsync(p => p.Id == command.Id);
                
                if(product == null)
                {
                    return default;
                }

                context.Products.Remove(product);
                await context.SaveChanges();

                return product.Id;
            }
        }

    }
}
