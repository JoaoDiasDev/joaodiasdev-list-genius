using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductSubGroupController(IProductSubGroupRepository productSubGroupRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductSubGroupDto>>> Get()
    {
        var productSubGroups = await productSubGroupRepository.GetAllAsync(
            psg => psg.ProductGroup);

        if (productSubGroups is null)
        {
            return NotFound("No Product Sub Groups.");
        }
        var productSubGroupsDto = mapper.Map<IEnumerable<ProductSubGroupDto>>(productSubGroups);
        return Ok(productSubGroupsDto);
    }

    [HttpGet("{id:int}", Name = "GetProductSubGroup")]
    public async Task<ActionResult<ProductSubGroupDto>> Get(int id)
    {
        var productSubGroup = await productSubGroupRepository.GetByIdAsync(id,
            psg => psg.ProductGroup);
        if (productSubGroup is null)
        {
            return NotFound($"ProductSubGroup with id {id} not found");
        }

        var productSubGroupDto = mapper.Map<ProductSubGroupDto>(productSubGroup);
        return Ok(productSubGroupDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductSubGroupDto productSubGroupDto)
    {
        if (productSubGroupDto is null)
        {
            return BadRequest("Invalid data.");
        }
        var productSubGroup = mapper.Map<ProductSubGroup>(productSubGroupDto);

        var productGroup = await productSubGroupRepository.FindByProperty<ProductGroup>("Name", productSubGroupDto.GroupName);
        if (productGroup is null)
        {
            return BadRequest($"ProductGroup '{productSubGroupDto.GroupName}' does not exist.");
        }
        productSubGroup.IdProductGroup = productGroup.Id;
        productSubGroup.ProductGroup = null!;

        await productSubGroupRepository.AddAsync(productSubGroup);

        return new CreatedAtRouteResult("GetProductSubGroup", new { id = productSubGroupDto.Id }, productSubGroupDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductSubGroupDto productSubGroupDto)
    {
        if (id != productSubGroupDto.Id)
        {
            return BadRequest($"id on query param is {id} different from productSubGroup id {productSubGroupDto.Id}");
        }

        if (productSubGroupDto is null)
        {
            return BadRequest("Invalid data");
        }

        var productSubGroup = await productSubGroupRepository.GetByIdAsync(id);

        if (productSubGroup is null)
        {
            return NotFound($"No Product Sub Group with id {id}.");
        }

        mapper.Map(productSubGroupDto, productSubGroup);


        try
        {
            var updated = await productSubGroupRepository.UpdateAsync(productSubGroup);
            if (!updated)
            {
                return Ok("No changes were detected.");
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await productSubGroupRepository.ExistsAsync<ProductSubGroup>(id))
            {
                return NotFound($"No Product Sub Group with id {id}.");
            }
            else
            {
                return Conflict("Concurrency conflict occurred while updating the product sub group. Please try again.");
            }
        }

        return Ok(productSubGroupDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var productSubGroup = await productSubGroupRepository.GetByIdAsync(id);

        if (productSubGroup is null)
        {
            return NotFound($"Product Sub Group with {id} not found");
        }

        await productSubGroupRepository.RemoveAsync(id);

        var productSubGroupDto = mapper.Map<ProductSubGroupDto>(productSubGroup);

        return Ok(productSubGroupDto);
    }
}