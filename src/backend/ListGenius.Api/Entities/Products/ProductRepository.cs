using ListGenius.Api.Context;
using ListGenius.Api.Entities.Bases;

namespace ListGenius.Api.Entities.Products;

public class ProductRepository(AppDbContext context)
    : BaseRepository<Product>(context), IProductRepository;
