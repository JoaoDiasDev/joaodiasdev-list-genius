using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SanctionTypeController(IGenericRepository<SanctionType> genericRepository)
    : GenericController<SanctionType>(genericRepository)
{
}
