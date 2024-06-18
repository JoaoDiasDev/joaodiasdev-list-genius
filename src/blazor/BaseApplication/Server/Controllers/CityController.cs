using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController(IGenericRepository<City> genericRepository)
    : GenericController<City>(genericRepository)
{
}