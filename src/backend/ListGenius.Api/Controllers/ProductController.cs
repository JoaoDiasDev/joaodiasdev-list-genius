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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        var products = await productRepository.GetAllAsync(
            p => p.ProductGroup,
            p => p.ProductSubGroup,
            p => p.ProductsList);

        if (products is null)
        {
            return NotFound("No products.");
        }
        var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(productsDto);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound($"Product with id {id} not found");
        }

        var productDto = mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDto productDto)
    {
        if (productDto is null)
        {
            return BadRequest("Invalid data.");
        }

        var product = mapper.Map<Product>(productDto);

        var productGroup = await productRepository.FindByProperty<ProductGroup>("Name", productDto.GroupName);
        if (productGroup is null)
        {
            return BadRequest($"ProductGroup '{productDto.GroupName}' does not exist.");
        }
        product.IdProductGroup = productGroup.Id;
        product.ProductGroup = productGroup;

        var productSubGroup = await productRepository.FindByProperty<ProductSubGroup>("Name", productDto.SubGroupName);
        if (productSubGroup is null)
        {
            return BadRequest($"ProductSubGroup '{productDto.SubGroupName}' does not exist.");
        }
        product.IdProductSubGroup = productSubGroup.Id;
        product.ProductSubGroup = productSubGroup;

        var productsList = await productRepository.FindByProperty<ProductsList>("Name", productDto.ShoppingListName);
        if (productsList is null)
        {
            return BadRequest($"ProductsList '{productDto.ShoppingListName}' does not exist.");
        }
        product.IdProductsList = productsList.Id;
        product.ProductsList = productsList;

        await productRepository.AddAsync(product);

        return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, productDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDto productDto)
    {
        if (id != productDto.Id)
        {
            return BadRequest($"{id} is different from product id {productDto.Id}");
        }

        if (productDto is null)
        {
            return BadRequest("Invalid data");
        }


        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound($"No Product with id {id}.");
        }

        mapper.Map(productDto, product);

        try
        {
            var updated = await productRepository.UpdateAsync(product);
            if (!updated)
            {
                return Ok("No changes were detected.");
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await productRepository.ExistsAsync<ProductGroup>(id))
            {
                return NotFound($"No Product with id {id}.");
            }
            else
            {
                return Conflict("Concurrency conflict occurred while updating the product. Please try again.");
            }
        }

        return Ok(productDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound($"Product with {id} not found");
        }

        await productRepository.RemoveAsync(id);

        var productDto = mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }
}
