using Asp.Versioning;
using ListGenius.Api.Entities.Users;
using Microsoft.AspNetCore.Authorization;

namespace ListGenius.Api.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsListController(IProductsListRepository productsListRepository, IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductsListDto>>> Get()
    {
        var productsLists = await productsListRepository.GetAllAsync(
            pl => pl.User!,
            pl => pl.Products);

        if (productsLists is null)
        {
            return NotFound("No Products Lists.");
        }

        var productsListsDto = mapper.Map<IEnumerable<ProductsListDto>>(productsLists);
        return Ok(productsListsDto);
    }

    [HttpGet("{id:int}", Name = "GetProductsList")]
    public async Task<ActionResult<ProductsListDto>> Get(int id)
    {
        var productsList = await productsListRepository.GetByIdAsync(id,
            pl => pl.User!,
            pl => pl.Products);

        if (productsList is null)
        {
            return NotFound($"ProductsList with id {id} not found");
        }

        var productsListDto = mapper.Map<ProductsListDto>(productsList);
        return Ok(productsListDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductsListDto productsListDto)
    {
        if (productsListDto is null)
        {
            return BadRequest("Invalid data.");
        }

        var productsList = mapper.Map<ProductsList>(productsListDto);

        var user = await userRepository.FindByFullNameAsync(productsList?.User?.FullName ?? string.Empty);
        if (user is null)
        {
            return BadRequest($"User '{productsListDto.UserName}' does not exist.");
        }

        if (productsList is not null)
        {
            productsList.IdUser = user.Id;
            productsList.User = null;

            var existentProductsList = new ProductsList();
            try
            {
                existentProductsList = await productsListRepository.FindByProperty<ProductsList>("Name", productsListDto.Name);
            }
            catch (Exception)
            {
                // ignored
            }

            if (existentProductsList.Id is not 0)
            {
                return BadRequest($"Products List with name {productsListDto.Name} already exists.");
            }

            await productsListRepository.AddAsync(productsList);


            return new CreatedAtRouteResult("GetProductsList", new { id = productsListDto.Id }, productsListDto);
        }

        return BadRequest($"Invalid Data.");
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductsListDto productsListDto)
    {
        if (id != productsListDto.Id)
        {
            return BadRequest($"{id} is different from Products List id {productsListDto.Id}");
        }

        if (productsListDto is null)
        {
            return BadRequest("Invalid data");
        }

        var productsList = await productsListRepository.GetByIdAsync(id);

        if (productsList == null)
        {
            return NotFound($"No Products List with id {id}.");
        }

        mapper.Map(productsListDto, productsList);

        try
        {
            var updated = await productsListRepository.UpdateAsync(productsList);
            if (!updated)
            {
                return Ok("No changes were detected.");
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await productsListRepository.ExistsAsync<ProductsList>(id))
            {
                return NotFound($"No Products List with id {id}.");
            }
            else
            {
                return Conflict("Concurrency conflict occurred while updating the productsList. Please try again.");
            }
        }

        return Ok(productsListDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var productsList = await productsListRepository.GetByIdAsync(id);
        if (productsList is null)
        {
            return NotFound($"ProductsList with {id} not found");
        }

        await productsListRepository.RemoveAsync(id);

        var productsListDto = mapper.Map<ProductsListDto>(productsList);

        return Ok(productsListDto);
    }
}
