using ListGenius.Api.Context;
using ListGenius.Api.Entities.Bases;

namespace ListGenius.Api.Entities.ProductsShared;

public class ProductSharedRepository(AppDbContext context)
    : BaseRepository<ProductShared>(context), IProductSharedRepository;
