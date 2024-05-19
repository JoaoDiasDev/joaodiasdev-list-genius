using Asp.Versioning;
using ListGenius.Domain.Business.Interfaces;
using ListGenius.Shared.VO.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenius.Web.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<GroupController> _logger;
        private IGroupBusiness _groupBusiness;

        public GroupController(ILogger<GroupController> logger, IGroupBusiness groupBusiness)
        {
            _logger = logger;
            _groupBusiness = groupBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<GroupVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_groupBusiness.FindWithPagedSearch(name ?? string.Empty, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(GroupVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get(long id)
        {
            var group = _groupBusiness.FindById(id);
            if (group == null)
            {
                return NotFound($"Group with {id} not found.");
            }

            return Ok(group);
        }

        [HttpGet("findGroupByName")]
        [ProducesResponseType(200, Type = typeof(List<GroupVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get([FromQuery] string name)
        {
            var group = _groupBusiness.FindByName(name: name);
            if (group == null)
            {
                return NotFound($"Group with {name} not found.");
            }

            return Ok(group);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(GroupVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] GroupVO group)
        {
            if (group == null)
            {
                return BadRequest("Inform group on body using json.");
            }

            return Ok(_groupBusiness.Create(group));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(GroupVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] GroupVO group)
        {
            if (group == null)
            {
                return BadRequest("Inform group on body using json.");
            }

            return Ok(_groupBusiness.Update(group));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Patch(long id)
        {
            var group = _groupBusiness.Disable(id);
            return Ok(group);
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

            _groupBusiness.Delete(id);
            return NoContent();
        }
    }
}