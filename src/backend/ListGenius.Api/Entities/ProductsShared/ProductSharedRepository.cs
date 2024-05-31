using Microsoft.Extensions.Caching.Memory;

namespace ListGenius.Api.Entities.ProductsShared;

public class ProductSharedRepository(AppDbContext context, IMemoryCache iMemoryCache)
    : BaseRepository<ProductShared>(context, iMemoryCache), IProductSharedRepository;
