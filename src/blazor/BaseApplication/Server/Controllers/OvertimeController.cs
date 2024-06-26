using BaseLibrary.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController(IGenericRepository<Overtime> genericRepository)
        : GenericController<Overtime>(genericRepository)
    {
    }
}
