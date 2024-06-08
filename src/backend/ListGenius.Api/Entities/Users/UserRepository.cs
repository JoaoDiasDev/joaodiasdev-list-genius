using ListGenius.Api.Entities.Users.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace ListGenius.Api.Entities.Users;

public class UserRepository(AppDbContext db, IMemoryCache cache) : IUserRepository
{
    private readonly MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(60));

    public async Task<ApplicationUser?> FindByFullNameAsync(string fullName)
    {
        var cacheKey = $"UserFullName_{fullName}";

        if (cache.TryGetValue(cacheKey, out ApplicationUser? cachedUser))
        {
            return cachedUser;
        }

        var user = await db.Users.AsSplitQuery().FirstOrDefaultAsync(u => u.FullName == fullName)
                   ?? throw new InvalidDataException($"No user found with {fullName}");

        cache.Set(cacheKey, user, _cacheOptions);

        return user;
    }

    public async Task<bool> UpdateImage(byte[] image, ImageType imageType)
    {

    }
}
