using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController(IGenericRepository<Department> genericRepository)
    : GenericController<Department>(genericRepository)
{
}