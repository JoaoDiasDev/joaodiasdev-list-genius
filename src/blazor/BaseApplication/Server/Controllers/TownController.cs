using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TownController(IGenericRepository<Town> genericRepository)
    : GenericController<Town>(genericRepository)
{
}