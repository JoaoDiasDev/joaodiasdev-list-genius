using Microsoft.Extensions.Caching.Memory;

namespace ListGenius.Api.Entities.ProductSubGroups;

public class ProductSubGroupRepository(AppDbContext context, IMemoryCache iMemoryCache)
    : BaseRepository<ProductSubGroup>(context, iMemoryCache), IProductSubGroupRepository;