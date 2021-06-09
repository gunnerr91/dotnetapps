using System;

namespace ProductApi.Models.Interfaces
{
    public interface IProductOptionsMapper
    {
        ProductOption Map(Guid productId, ProductOptionsDto productOptionsDto);
    }
}
