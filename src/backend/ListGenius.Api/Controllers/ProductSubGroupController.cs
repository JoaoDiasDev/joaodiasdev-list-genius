using Asp.Versioning;
using AutoMapper;
using ListGenius.Api.Entities.ProductSubGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductSubGroupController(IProductSubGroupRepository productSubGroupRepository, IMapper mapper) : ControllerBase
{
    private readonly IProductSubGroupRepository _productSubGroupRepository = productSubGroupRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductSubGroupDTO>>> Get()
    {
        var productSubGroups = await _productSubGroupRepository.GetAllAsync();
        if (productSubGroups is null)
        {
            return NotFound("No Product Sub Groups.");
        }
        var productSubGroupsDto = _mapper.Map<IEnumerable<ProductSubGroupDTO>>(productSubGroups);
        return Ok(productSubGroupsDto);
    }

    [HttpGet("{id:int}", Name = "GetProductSubGroup")]
    public async Task<ActionResult<ProductSubGroupDTO>> Get(int id)
    {
        var productSubGroup = await _productSubGroupRepository.GetByIdAsync(id);
        if (productSubGroup is null)
        {
            return NotFound($"ProductSubGroup with {id} not found");
        }

        var productSubGroupDto = _mapper.Map<ProductSubGroupDTO>(productSubGroup);
        return Ok(productSubGroupDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductSubGroupDTO productSubGroupDTO)
    {
        if (productSubGroupDTO is null)
        {
            return BadRequest("Invalid data.");
        }

        var productSubGroup = _mapper.Map<ProductSubGroup>(productSubGroupDTO);

        await _productSubGroupRepository.AddAsync(productSubGroup);

        return new CreatedAtRouteResult("GetProductSubGroup", new { id = productSubGroupDTO.Id }, productSubGroupDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductSubGroupDTO productSubGroupDTO)
    {
        if (id != productSubGroupDTO.Id)
        {
            return BadRequest($"{id} is different from productSubGroup id {productSubGroupDTO.Id}");
        }

        if (productSubGroupDTO is null)
        {
            return BadRequest("Invalid data");
        }

        var productSubGroup = _mapper.Map<ProductSubGroup>(productSubGroupDTO);

        await _productSubGroupRepository.UpdateAsync(productSubGroup);

        return Ok(productSubGroupDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var productSubGroup = await _productSubGroupRepository.GetByIdAsync(id);
        if (productSubGroup is null)
        {
            return NotFound($"ProductSubGroup {id} not found");
        }

        await _productSubGroupRepository.RemoveAsync(id);

        return Ok(productSubGroup);
    }
}