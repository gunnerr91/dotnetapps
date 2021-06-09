using Microsoft.AspNetCore.Mvc;
using System;

namespace ProductApi.Models
{
    public class ProductDto
    {
        [FromBody]
        public string Name { get; set; }
        [FromBody]
        public string Description { get; set; }
        [FromBody]
        public decimal Price { get; set; }
        [FromBody]
        public decimal DeliveryPrice { get; set; }
    }
}
