using ListGenius.Api.Context;
using ListGenius.Api.Entities.Bases;

namespace ListGenius.Api.Entities.ProductSubGroups;

public class ProductSubGroupRepository(AppDbContext context)
    : BaseRepository<ProductSubGroup>(context), IProductSubGroupRepository;