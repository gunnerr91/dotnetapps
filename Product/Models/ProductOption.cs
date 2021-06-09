using System;

namespace ProductApi.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
    }
}
