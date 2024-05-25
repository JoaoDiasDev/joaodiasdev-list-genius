using Asp.Versioning;
using AutoMapper;
using ListGenius.Api.Entities.ProductGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductGroupController(IProductGroupRepository productGroupRepository, IMapper mapper) : ControllerBase
{
    private readonly IProductGroupRepository _productGroupRepository = productGroupRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductGroupDTO>>> Get()
    {
        var productGroups = await _productGroupRepository.GetAllAsync();
        if (productGroups is null)
        {
            return NotFound("No Product Groups.");
        }
        var productGroupsDto = _mapper.Map<IEnumerable<ProductGroupDTO>>(productGroups);
        return Ok(productGroupsDto);
    }

    [HttpGet("{id:int}", Name = "GetProductGroup")]
    public async Task<ActionResult<ProductGroupDTO>> Get(int id)
    {
        var productGroup = await _productGroupRepository.GetByIdAsync(id);
        if (productGroup is null)
        {
            return NotFound($"ProductGroup with {id} not found");
        }

        var productGroupDto = _mapper.Map<ProductGroupDTO>(productGroup);
        return Ok(productGroupDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductGroupDTO productGroupDTO)
    {
        if (productGroupDTO is null)
        {
            return BadRequest("Invalid data.");
        }

        var productGroup = _mapper.Map<ProductGroup>(productGroupDTO);

        await _productGroupRepository.AddAsync(productGroup);

        return new CreatedAtRouteResult("GetProductGroup", new { id = productGroupDTO.Id }, productGroupDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductGroupDTO productGroupDTO)
    {
        if (id != productGroupDTO.Id)
        {
            return BadRequest($"{id} is different from productGroup id {productGroupDTO.Id}");
        }

        if (productGroupDTO is null)
        {
            return BadRequest("Invalid data");
        }

        var productGroup = _mapper.Map<ProductGroup>(productGroupDTO);

        await _productGroupRepository.UpdateAsync(productGroup);

        return Ok(productGroupDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var productGroup = await _productGroupRepository.GetByIdAsync(id);
        if (productGroup is null)
        {
            return NotFound($"ProductGroup {id} not found");
        }

        await _productGroupRepository.RemoveAsync(id);

        return Ok(productGroup);
    }
}
