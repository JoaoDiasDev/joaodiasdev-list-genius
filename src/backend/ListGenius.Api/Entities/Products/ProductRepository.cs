using Microsoft.Extensions.Caching.Memory;

namespace ListGenius.Api.Entities.Products;

public class ProductRepository(AppDbContext context, IMemoryCache iMemoryCache)
    : BaseRepository<Product>(context, iMemoryCache), IProductRepository;
