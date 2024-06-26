using BaseLibrary.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController(IGenericRepository<Doctor> genericRepository)
        : GenericController<Doctor>(genericRepository)
    {
    }
}
