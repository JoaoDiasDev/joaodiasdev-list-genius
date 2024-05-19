using Asp.Versioning;
using ListGenius.Domain.Business.Interfaces;
using ListGenius.Shared.VO.ProductList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenius.Web.Controllers
{

    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsListController : ControllerBase
    {

        private readonly ILogger<ProductsListController> _logger;

        private IProductsListBusiness _productsListBusiness;

        public ProductsListController(ILogger<ProductsListController> logger, IProductsListBusiness productsListBusiness)
        {
            _logger = logger;
            _productsListBusiness = productsListBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<ProductsListVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get(
            [FromQuery] string name,
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_productsListBusiness.FindWithPagedSearch(name ?? string.Empty, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProductsListVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get(long id)
        {
            var productList = _productsListBusiness.FindByID(id);
            if (productList == null) return NotFound($"Product List with {id} not found.");
            return Ok(productList);
        }

        [HttpGet("findProductListByName")]
        [ProducesResponseType(200, Type = typeof(List<ProductsListVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get([FromQuery] string name)
        {
            var productList = _productsListBusiness.FindByName(name: name);
            if (productList == null)
            {
                return NotFound($"Product with {name} not found.");
            }

            return Ok(productList);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProductsListVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] ProductsListVO productList)
        {
            if (productList == null) return BadRequest();
            return Ok(_productsListBusiness.Create(productList));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ProductsListVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Put([FromBody] ProductsListVO productList)
        {
            if (productList == null) return BadRequest();
            return Ok(_productsListBusiness.Update(productList));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]

        public IActionResult Patch(long id)
        {
            var productList = _productsListBusiness.Disable(id);
            return Ok(productList);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Delete(long id)
        {
            _productsListBusiness.Delete(id);
            return NoContent();
        }
    }
}
