using BaseLibrary.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionController(IGenericRepository<Sanction> genericRepository) : GenericController<Sanction>(genericRepository)
    {
    }
}
