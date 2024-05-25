using ListGenius.Api.Context;
using ListGenius.Api.Entities.Bases;

namespace ListGenius.Api.Entities.ProductGroups;

public class ProductGroupRepository(AppDbContext context)
    : BaseRepository<ProductGroup>(context), IProductGroupRepository;
