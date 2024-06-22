using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IGenericRepository<Employee> genericRepository) : GenericController<Employee>(genericRepository)
{
}