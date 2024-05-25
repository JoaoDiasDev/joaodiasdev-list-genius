using ListGenius.Api.Context;
using ListGenius.Api.Entities.Bases;

namespace ListGenius.Api.Entities.ProductShareds;

public class ProductSharedRepository(AppDbContext context)
    : BaseRepository<ProductShared>(context), IProductSharedRepository;
