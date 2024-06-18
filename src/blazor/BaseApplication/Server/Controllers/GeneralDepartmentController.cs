using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneralDepartmentController(IGenericRepository<GeneralDepartment> genericRepository)
    : GenericController<GeneralDepartment>(genericRepository)
{
}