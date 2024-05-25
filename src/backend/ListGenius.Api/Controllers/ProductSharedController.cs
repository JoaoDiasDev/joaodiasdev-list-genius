using Asp.Versioning;
using AutoMapper;
using ListGenius.Api.Entities.ProductShareds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var productShareds = await _productSharedRepository.GetAllAsync();
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
            return NotFound($"ProductShared with {id} not found");
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

        var productShared = _mapper.Map<ProductShared>(productSharedDTO);

        await _productSharedRepository.UpdateAsync(productShared);

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

        return Ok(productShared);
    }
}
