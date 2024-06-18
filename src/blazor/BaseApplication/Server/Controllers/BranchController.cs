using BaseLibrary.Entities;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchController(IGenericRepository<Branch> genericRepository)
    : GenericController<Branch>(genericRepository)
{
}