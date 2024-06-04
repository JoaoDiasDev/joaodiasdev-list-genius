using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductSharedController(IProductSharedRepository productSharedRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductSharedDto>>> Get()
    {
        var productShareds = await productSharedRepository.GetAllAsync(
            p => p.ProductGroup,
            p => p.ProductSubGroup);

        if (productShareds is null)
        {
            return NotFound("No Products.");
        }
        var productSharedsDto = mapper.Map<IEnumerable<ProductSharedDto>>(productShareds);
        return Ok(productSharedsDto);
    }

    [HttpGet("{id:int}", Name = "GetProductShared")]
    public async Task<ActionResult<ProductSharedDto>> Get(int id)
    {
        var productShared = await productSharedRepository.GetByIdAsync(id,
            p => p.ProductGroup,
            p => p.ProductSubGroup);

        if (productShared is null)
        {
            return NotFound($"ProductShared with id {id} not found");
        }

        var productSharedDto = mapper.Map<ProductSharedDto>(productShared);
        return Ok(productSharedDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductSharedDto productSharedDto)
    {
        if (productSharedDto is null)
        {
            return BadRequest("Invalid data.");
        }

        var productShared = mapper.Map<ProductShared>(productSharedDto);

        var productGroup = await productSharedRepository.FindByProperty<ProductGroup>("Name", productSharedDto.GroupName);
        if (productGroup is null)
        {
            return BadRequest($"ProductGroup '{productSharedDto.GroupName}' does not exist.");
        }
        productShared.IdProductGroup = productGroup.Id;
        productShared.ProductGroup = null!;

        var productSubGroup = await productSharedRepository.FindByProperty<ProductSubGroup>("Name", productSharedDto.SubGroupName);
        if (productSubGroup is null)
        {
            return BadRequest($"ProductSubGroup '{productSharedDto.SubGroupName}' does not exist.");
        }
        productShared.IdProductSubGroup = productSubGroup.Id;
        productShared.ProductSubGroup = null!;

        var existentProductShared = new ProductShared();
        try
        {
            existentProductShared = await productSharedRepository.FindByProperty<ProductShared>("Name", productSharedDto.Name);
        }
        catch (Exception)
        {
            // ignored
        }

        if (existentProductShared.Id is not 0)
        {
            return BadRequest($"Product Shared with name {productSharedDto.Name} already exists.");
        }

        await productSharedRepository.AddAsync(productShared);

        return new CreatedAtRouteResult("GetProductShared", new { id = productSharedDto.Id }, productSharedDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductSharedDto productSharedDto)
    {
        if (id != productSharedDto.Id)
        {
            return BadRequest($"{id} is different from productShared id {productSharedDto.Id}");
        }

        if (productSharedDto is null)
        {
            return BadRequest("Invalid data");
        }

        var productShared = await productSharedRepository.GetByIdAsync(id);

        if (productShared == null)
        {
            return NotFound($"No Product Shared with id {id}.");
        }

        mapper.Map(productSharedDto, productShared);

        try
        {
            var updated = await productSharedRepository.UpdateAsync(productShared);
            if (!updated)
            {
                return Ok("No changes were detected.");
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await productSharedRepository.ExistsAsync<ProductShared>(id))
            {
                return NotFound($"No Product Shared with id {id}.");
            }
            else
            {
                return Conflict("Concurrency conflict occurred while updating the Product Shared. Please try again.");
            }
        }

        return Ok(productSharedDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var productShared = await productSharedRepository.GetByIdAsync(id);
        if (productShared is null)
        {
            return NotFound($"ProductShared with {id} not found");
        }

        await productSharedRepository.RemoveAsync(id);

        var productSharedDto = mapper.Map<ProductSharedDto>(productShared);

        return Ok(productSharedDto);
    }
}
