using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OvertimeTypeController(IGenericRepository<OvertimeType> genericRepository)
    : GenericController<OvertimeType>(genericRepository)
{
}
