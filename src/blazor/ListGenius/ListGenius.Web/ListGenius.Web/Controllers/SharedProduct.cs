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
    public class SharedProductController : ControllerBase
    {
        private readonly ILogger<SharedProductController> _logger;
        private ISharedProductBusiness _sharedProductBusiness;

        public SharedProductController(ILogger<SharedProductController> logger, ISharedProductBusiness sharedProductBusiness)
        {
            _logger = logger;
            _sharedProductBusiness = sharedProductBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<SharedProductVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_sharedProductBusiness.FindWithPagedSearch(name ?? string.Empty, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SharedProductVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var sharedProduct = _sharedProductBusiness.FindById(id);
            if (sharedProduct == null)
            {
                return NotFound($"SharedProduct with {id} not found.");
            }

            return Ok(sharedProduct);
        }

        [HttpGet("findSharedProductByName")]
        [ProducesResponseType(200, Type = typeof(List<SharedProductVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get([FromQuery] string name)
        {
            var sharedProduct = _sharedProductBusiness.FindByName(name: name);
            if (sharedProduct == null)
            {
                return NotFound($"SharedProduct with {name} not found.");
            }

            return Ok(sharedProduct);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(SharedProductVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] SharedProductVO sharedProduct)
        {
            if (sharedProduct == null)
            {
                return BadRequest("Inform sharedProduct on body using json.");
            }

            return Ok(_sharedProductBusiness.Create(sharedProduct));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(SharedProductVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] SharedProductVO sharedProduct)
        {
            if (sharedProduct == null)
            {
                return BadRequest("Inform sharedProduct on body using json.");
            }

            return Ok(_sharedProductBusiness.Update(sharedProduct));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]

        public IActionResult Patch(long id)
        {
            var sharedProduct = _sharedProductBusiness.Disable(id);
            return Ok(sharedProduct);
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

            _sharedProductBusiness.Delete(id);
            return NoContent();
        }
    }
}
