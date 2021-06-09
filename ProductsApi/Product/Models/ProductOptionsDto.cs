using Microsoft.AspNetCore.Mvc;
using System;

namespace ProductApi.Models
{
    public class ProductOptionsDto
    {
        [FromBody]
        public string Name { get; set; }
        [FromBody]
        public string Description { get; set; }
    }
}
