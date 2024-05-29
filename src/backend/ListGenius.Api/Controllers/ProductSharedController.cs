using Asp.Versioning;
using AutoMapper;
using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.ProductShareds;
using ListGenius.Api.Entities.ProductSubGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductSharedController(IProductSharedRepository productSharedRepository, IMapper mapper) : ControllerBase
{
    private readonly IProductSharedRepository _productSharedRepository = productSharedRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductSharedDTO>>> Get()
    {
        var productShareds = await _productSharedRepository.GetAllAsync(
            p => p.ProductGroup,
            p => p.ProductSubGroup);

        if (productShareds is null)
        {
            return NotFound("No Products.");
        }
        var productSharedsDto = _mapper.Map<IEnumerable<ProductSharedDTO>>(productShareds);
        return Ok(productSharedsDto);
    }

    [HttpGet("{id:int}", Name = "GetProductShared")]
    public async Task<ActionResult<ProductSharedDTO>> Get(int id)
    {
        var productShared = await _productSharedRepository.GetByIdAsync(id);
        if (productShared is null)
        {
            return NotFound($"ProductShared with id {id} not found");
        }

        var productSharedDto = _mapper.Map<ProductSharedDTO>(productShared);
        return Ok(productSharedDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductSharedDTO productSharedDTO)
    {
        if (productSharedDTO is null)
        {
            return BadRequest("Invalid data.");
        }

        var productShared = _mapper.Map<ProductShared>(productSharedDTO);

        var productGroup = await _productSharedRepository.FindByName<ProductGroup>(productSharedDTO.GroupName);
        if (productGroup is null)
        {
            return BadRequest($"ProductGroup '{productSharedDTO.GroupName}' does not exist.");
        }
        productShared.IdProductGroup = productGroup.Id;
        productShared.ProductGroup = productGroup;

        var productSubGroup = await _productSharedRepository.FindByName<ProductSubGroup>(productSharedDTO.SubGroupName);
        if (productSubGroup is null)
        {
            return BadRequest($"ProductSubGroup '{productSharedDTO.SubGroupName}' does not exist.");
        }
        productShared.IdProductSubGroup = productSubGroup.Id;
        productShared.ProductSubGroup = productSubGroup;

        await _productSharedRepository.AddAsync(productShared);

        return new CreatedAtRouteResult("GetProductShared", new { id = productSharedDTO.Id }, productSharedDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductSharedDTO productSharedDTO)
    {
        if (id != productSharedDTO.Id)
        {
            return BadRequest($"{id} is different from productShared id {productSharedDTO.Id}");
        }

        if (productSharedDTO is null)
        {
            return BadRequest("Invalid data");
        }

        var productShared = await _productSharedRepository.GetByIdAsync(id);

        if (productShared == null)
        {
            return NotFound($"No Product Shared with id {id}.");
        }

        _mapper.Map(productSharedDTO, productShared);

        try
        {
            bool updated = await _productSharedRepository.UpdateAsync(productShared);
            if (!updated)
            {
                return Ok("No changes were detected.");
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _productSharedRepository.ExistsAsync(id))
            {
                return NotFound($"No Product Shared with id {id}.");
            }
            else
            {
                return Conflict("Concurrency conflict occurred while updating the Product Shared. Please try again.");
            }
        }

        return Ok(productSharedDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var productShared = await _productSharedRepository.GetByIdAsync(id);
        if (productShared is null)
        {
            return NotFound($"ProductShared {id} not found");
        }

        await _productSharedRepository.RemoveAsync(id);

        var productSharedDTO = _mapper.Map<ProductSharedDTO>(productShared);

        return Ok(productSharedDTO);
    }
}
