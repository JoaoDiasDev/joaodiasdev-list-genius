using BaseLibrary.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationTypeController(IGenericRepository<VacationType> genericRepository)
        : GenericController<VacationType>(genericRepository)
    {
    }
}
