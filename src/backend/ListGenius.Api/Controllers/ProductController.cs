using Asp.Versioning;
using AutoMapper;
using ListGenius.Api.Entities.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController(IProductRepository productRepository, IMapper mapper) : ControllerBase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var products = await _productRepository.GetAllAsync();
        if (products is null)
        {
            return NotFound("No products.");
        }
        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(productsDto);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound($"Product with {id} not found");
        }

        var productDto = _mapper.Map<ProductDTO>(product);
        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
    {
        if (productDTO is null)
        {
            return BadRequest("Invalid data.");
        }

        var product = _mapper.Map<Product>(productDTO);

        await _productRepository.AddAsync(product);

        return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
    {
        if (id != productDTO.Id)
        {
            return BadRequest($"{id} is different from product id {productDTO.Id}");
        }

        if (productDTO is null)
        {
            return BadRequest("Invalid data");
        }

        var product = _mapper.Map<Product>(productDTO);

        await _productRepository.UpdateAsync(product);

        return Ok(productDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound($"Product {id} not found");
        }

        await _productRepository.RemoveAsync(id);

        return Ok(product);
    }
}
