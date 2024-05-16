using JoaoDiasDev.ListGenius.Business.Interfaces;
using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoaoDiasDev.ListGenius.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubGroupController : ControllerBase
    {
        private readonly ILogger<SubGroupController> _logger;
        private ISubGroupBusiness _subGroupBusiness;

        public SubGroupController(ILogger<SubGroupController> logger, ISubGroupBusiness subGroupBusiness)
        {
            _logger = logger;
            _subGroupBusiness = subGroupBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<SubGroupVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_subGroupBusiness.FindWithPagedSearch(name ?? string.Empty, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SubGroupVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var subGroup = _subGroupBusiness.FindById(id);
            if (subGroup == null)
            {
                return NotFound($"SubGroup with {id} not found.");
            }

            return Ok(subGroup);
        }

        [HttpGet("findSubGroupByName")]
        [ProducesResponseType(200, Type = typeof(List<SubGroupVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string name)
        {
            var subGroup = _subGroupBusiness.FindByName(name: name);
            if (subGroup == null)
            {
                return NotFound($"SubGroup with {name} not found.");
            }

            return Ok(subGroup);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(SubGroupVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] SubGroupVO subGroup)
        {
            if (subGroup == null)
            {
                return BadRequest("Inform subGroup on body using json.");
            }

            return Ok(_subGroupBusiness.Create(subGroup));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(SubGroupVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] SubGroupVO subGroup)
        {
            if (subGroup == null)
            {
                return BadRequest("Inform subGroup on body using json.");
            }

            return Ok(_subGroupBusiness.Update(subGroup));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            var subGroup = _subGroupBusiness.Disable(id);
            return Ok(subGroup);
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

            _subGroupBusiness.Delete(id);
            return NoContent();
        }
    }
}