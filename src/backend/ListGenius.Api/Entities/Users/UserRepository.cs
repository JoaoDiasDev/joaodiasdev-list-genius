using ListGenius.Api.Context;
using Microsoft.EntityFrameworkCore;

namespace ListGenius.Api.Entities.Users;
public class UserRepository(AppDbContext db) : IUserRepository
{
    public async Task<ApplicationUser> FindByFullNameAsync(string fullName)
    {
        var result = await db.Users.FirstOrDefaultAsync(u => u.FullName == fullName);
        return result ?? throw new InvalidDataException($"No user found with {fullName}");
    }

}

