namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenericController<T>(IGenericRepository<T> genericRepository) : Controller where T : class
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await genericRepository.GetAll());
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Invalid request sent");
        return Ok(await genericRepository.DeleteById(id));
    }

    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0) return BadRequest("Invalid request sent");
        return Ok(await genericRepository.GetById(id));
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(T model)
    {
        if (model is null) return BadRequest("Invalid request sent");
        return Ok(await genericRepository.Create(model));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(T model)
    {
        if (model is null) return BadRequest("Invalid request sent");
        return Ok(await genericRepository.Update(model));
    }
}