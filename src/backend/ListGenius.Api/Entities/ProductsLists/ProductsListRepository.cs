using Microsoft.Extensions.Caching.Memory;

namespace ListGenius.Api.Entities.ProductsLists;

public class ProductsListRepository(AppDbContext context, IMemoryCache iMemoryCache)
    : BaseRepository<ProductsList>(context, iMemoryCache), IProductsListRepository;