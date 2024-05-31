using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductGroupController(IProductGroupRepository productGroupRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductGroupDto>>> Get()
    {
        var productGroups = await productGroupRepository.GetAllAsync();

        if (productGroups is null)
        {
            return NotFound("No Product Groups.");
        }
        var productGroupsDto = mapper.Map<IEnumerable<ProductGroupDto>>(productGroups);
        return Ok(productGroupsDto);
    }

    [HttpGet("{id:int}", Name = "GetProductGroup")]
    public async Task<ActionResult<ProductGroupDto>> Get(int id)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(id);
        if (productGroup is null)
        {
            return NotFound($"ProductGroup with {id} not found");
        }

        var productGroupDto = mapper.Map<ProductGroupDto>(productGroup);
        return Ok(productGroupDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductGroupDto productGroupDto)
    {
        if (productGroupDto is null)
        {
            return BadRequest("Invalid data.");
        }

        var productGroup = mapper.Map<ProductGroup>(productGroupDto);

        await productGroupRepository.AddAsync(productGroup);

        return new CreatedAtRouteResult("GetProductGroup", new { id = productGroupDto.Id }, productGroupDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductGroupDto productGroupDto)
    {
        if (id != productGroupDto.Id)
        {
            return BadRequest($"{id} is different from productGroup id {productGroupDto.Id}");
        }

        if (productGroupDto is null)
        {
            return BadRequest("Invalid data");
        }

        var productGroup = await productGroupRepository.GetByIdAsync(id);

        if (productGroup is null)
        {
            return NotFound($"No Product Group with {id}.");
        }

        mapper.Map(productGroupDto, productGroup);

        try
        {
            var updated = await productGroupRepository.UpdateAsync(productGroup);
            if (!updated)
            {
                return Ok($"No changes were detected.");
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await productGroupRepository.ExistsAsync<ProductGroup>(id))
            {
                return NotFound($"No Product with id {id}.");
            }
            else
            {
                return Conflict("Concurrency conflict occurred while updating the product. Please try again.");
            }
        }


        return Ok(productGroupDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(id);
        if (productGroup is null)
        {
            return NotFound($"ProductGroup {id} not found");
        }

        await productGroupRepository.RemoveAsync(id);

        return Ok(productGroup);
    }
}
