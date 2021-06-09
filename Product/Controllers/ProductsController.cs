using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Models.Interfaces;
using ProductApi.Repositories.Interface;
using System;
using System.Linq;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        private IProductOptionsRepository _productOptionsRepository;
        private IProductMapper _productMapper;
        private IProductOptionsMapper _productOptionsMapper;

        public ProductsController(IProductRepository productRepository, IProductMapper mapper, IProductOptionsRepository productOptionsrepository, IProductOptionsMapper productOptionsMapper)
        {
            _productRepository = productRepository;
            _productMapper = mapper;
            _productOptionsRepository = productOptionsrepository;
            _productOptionsMapper = productOptionsMapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productRepository.GetAllProducts());
        }

        [HttpGet("name/{name}")]
        public IActionResult Get(string name)
        {
            var product = _productRepository.GetProductByName(name);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductDto product)
        {
            _productRepository.AddProduct(_productMapper.Map(product));
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]ProductDto product)
        {
            if (_productRepository.GetProductById(id) == null)
            {
                return NotFound();
            }
            _productRepository.UpdateProduct(id, _productMapper.Map(product));
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {

            if (_productRepository.GetProductById(id) == null)
            {
                return NotFound();
            }
            _productRepository.DeleteProduct(id);
            return Ok();

        }

        [HttpGet("{id}/options")]
        public IActionResult GetOptions(Guid id)
        {
            var productOptions = _productOptionsRepository.GetAllProductOptions(id);

            if (productOptions.Count() == 0)
            {
                return NotFound();
            }

            return Ok(_productOptionsRepository.GetAllProductOptions(id));
        }

        [HttpGet("{id}/options/{optionId}")]
        public IActionResult GetOptions(Guid id, Guid optionId)
        {
            var product = _productOptionsRepository.GetProductOptionById(id, optionId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("{id}/options")]
        public IActionResult PostOption(Guid id, [FromBody]ProductOptionsDto productOptionsDto)
        {
            var productOption = _productOptionsMapper.Map(id, productOptionsDto);
            _productOptionsRepository.AddProductOption(productOption);

            return Ok();
        }

        [HttpPut("{id}/options/{optionId}")]
        public IActionResult PutOption(Guid id, Guid optionId, [FromBody]ProductOptionsDto productOptionDto)
        {
            if(_productOptionsRepository.GetProductOptionById(id, optionId) == null)
            {
                return NotFound();
            }

            _productOptionsRepository.UpdateProductOption(optionId, _productOptionsMapper.Map(id, productOptionDto));

            return Ok();
        }

        [HttpDelete("{id}/options/{optionId}")]
        public IActionResult DeleteOption(Guid id, Guid optionId)
        {
            if(_productOptionsRepository.GetProductOptionById(id, optionId) == null)
            {
                return NotFound();
            }

            _productOptionsRepository.DeleteProductOption(id, optionId);

            return Ok();
        }
    }
}