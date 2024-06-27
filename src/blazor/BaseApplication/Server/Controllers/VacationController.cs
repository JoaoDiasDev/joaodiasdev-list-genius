using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacationController(IGenericRepository<Vacation> genericRepository)
    : GenericController<Vacation>(genericRepository)
{
}
