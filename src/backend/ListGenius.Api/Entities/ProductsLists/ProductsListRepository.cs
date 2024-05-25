using ListGenius.Api.Context;
using ListGenius.Api.Entities.Bases;

namespace ListGenius.Api.Entities.ProductsLists;

public class ProductsListRepository(AppDbContext context)
    : BaseRepository<ProductsList>(context), IProductsListRepository;