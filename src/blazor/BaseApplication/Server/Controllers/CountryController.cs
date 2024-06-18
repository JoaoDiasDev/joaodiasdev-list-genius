using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController(IGenericRepository<Country> genericRepository)
    : GenericController<Country>(genericRepository)
{
}