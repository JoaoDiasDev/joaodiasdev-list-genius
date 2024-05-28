using Asp.Versioning;
using AutoMapper;
using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.ProductsLists;
using ListGenius.Api.Entities.ProductSubGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var products = await _productRepository.GetAllAsync(
            p => p.ProductGroup,
            p => p.ProductSubGroup,
            p => p.ProductsList);

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

        var productGroup = await _productRepository.FindByName<ProductGroup>(productDTO.GroupName);
        if (productGroup is null)
        {
            return BadRequest($"ProductGroup '{productDTO.GroupName}' does not exist.");
        }
        product.IdProductGroup = productGroup.Id;
        product.ProductGroup = productGroup;

        var productSubGroup = await _productRepository.FindByName<ProductSubGroup>(productDTO.SubGroupName);
        if (productSubGroup is null)
        {
            return BadRequest($"ProductSubGroup '{productDTO.SubGroupName}' does not exist.");
        }
        product.IdProductSubGroup = productSubGroup.Id;
        product.ProductSubGroup = productSubGroup;

        var productsList = await _productRepository.FindByName<ProductsList>(productDTO.ShoppingListName);
        if (productsList is null)
        {
            return BadRequest($"ProductsList '{productDTO.ShoppingListName}' does not exist.");
        }
        product.IdProductsList = productsList.Id;
        product.ProductsList = productsList;

        await _productRepository.AddAsync(product);

        return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, productDTO);
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


        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _mapper.Map(productDTO, product);

        try
        {
            bool updated = await _productRepository.UpdateAsync(product);
            if (!updated)
            {
                return Ok("No changes were detected.");
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _productRepository.ExistsAsync(id))
            {
                return NotFound();
            }
            else
            {
                return Conflict("Concurrency conflict occurred while updating the product. Please try again.");
            }
        }

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

        var productDTO = _mapper.Map<ProductDTO>(product);
        return Ok(productDTO);
    }
}
