using Asp.Versioning;
using AutoMapper;
using ListGenius.Api.Entities.ProductsLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsListController(IProductsListRepository productsListRepository, IMapper mapper) : ControllerBase
{
    private readonly IProductsListRepository _productsListRepository = productsListRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductsListDTO>>> Get()
    {
        var productsLists = await _productsListRepository.GetAllAsync();
        if (productsLists is null)
        {
            return NotFound("No Products Lists.");
        }
        var productsListsDto = _mapper.Map<IEnumerable<ProductsListDTO>>(productsLists);
        return Ok(productsListsDto);
    }

    [HttpGet("{id:int}", Name = "GetProductsList")]
    public async Task<ActionResult<ProductsListDTO>> Get(int id)
    {
        var productsList = await _productsListRepository.GetByIdAsync(id);
        if (productsList is null)
        {
            return NotFound($"ProductsList with {id} not found");
        }

        var productsListDto = _mapper.Map<ProductsListDTO>(productsList);
        return Ok(productsListDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductsListDTO productsListDTO)
    {
        if (productsListDTO is null)
        {
            return BadRequest("Invalid data.");
        }

        var productsList = _mapper.Map<ProductsList>(productsListDTO);

        await _productsListRepository.AddAsync(productsList);

        return new CreatedAtRouteResult("GetProductsList", new { id = productsListDTO.Id }, productsListDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductsListDTO productsListDTO)
    {
        if (id != productsListDTO.Id)
        {
            return BadRequest($"{id} is different from productsList id {productsListDTO.Id}");
        }

        if (productsListDTO is null)
        {
            return BadRequest("Invalid data");
        }

        var productsList = _mapper.Map<ProductsList>(productsListDTO);

        await _productsListRepository.UpdateAsync(productsList);

        return Ok(productsListDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var productsList = await _productsListRepository.GetByIdAsync(id);
        if (productsList is null)
        {
            return NotFound($"ProductsList {id} not found");
        }

        await _productsListRepository.RemoveAsync(id);

        return Ok(productsList);
    }
}
