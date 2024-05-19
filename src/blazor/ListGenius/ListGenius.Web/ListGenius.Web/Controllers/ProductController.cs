using Asp.Versioning;
using ListGenius.Domain.Business.Interfaces;
using ListGenius.Shared.VO.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenius.Web.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IProductBusiness _productBusiness;

        public ProductController(ILogger<ProductController> logger, IProductBusiness productBusiness)
        {
            _logger = logger;
            _productBusiness = productBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<ProductVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_productBusiness.FindWithPagedSearch(name ?? string.Empty, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProductVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get(long id)
        {
            var product = _productBusiness.FindById(id);
            if (product == null)
            {
                return NotFound($"Product with {id} not found.");
            }

            return Ok(product);
        }

        [HttpGet("findProductByName")]
        [ProducesResponseType(200, Type = typeof(List<ProductVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get([FromQuery] string name)
        {
            var product = _productBusiness.FindByName(name: name);
            if (product == null)
            {
                return NotFound($"Product with {name} not found.");
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProductVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] ProductVO product)
        {
            if (product == null)
            {
                return BadRequest("Inform product on body using json.");
            }

            return Ok(_productBusiness.Create(product));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ProductVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Put([FromBody] ProductVO product)
        {
            if (product == null)
            {
                return BadRequest("Inform product on body using json.");
            }

            return Ok(_productBusiness.Update(product));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]

        public IActionResult Patch(long id)
        {
            var product = _productBusiness.Disable(id);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                return BadRequest("Sent id was incorrect.");
            }

            _productBusiness.Delete(id);
            return NoContent();
        }
    }
}