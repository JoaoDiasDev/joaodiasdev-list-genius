using Microsoft.Extensions.Caching.Memory;

namespace ListGenius.Api.Entities.ProductGroups;

public class ProductGroupRepository(AppDbContext context, IMemoryCache iMemoryCache)
    : BaseRepository<ProductGroup>(context, iMemoryCache), IProductGroupRepository;
