using ListGenius.Domain.Context;
using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.SharedProductRepo
{
    public class SharedProductRepository : GenericRepository<ProductShared>, ISharedProductRepository
    {
        public SharedProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ProductShared Disable(long id)
        {
            if (!_context.ProductsShared.Any(p => p.Id.Equals(id)))
            {
                return new ProductShared();
            }

            var subGroup = _context.ProductsShared.SingleOrDefault(p => p.Id.Equals(id));

            if (subGroup != null)
            {
                subGroup.Enabled = false;
                try
                {
                    _context.Entry(subGroup).CurrentValues.SetValues(subGroup);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return subGroup ?? new ProductShared();
        }

        public List<ProductShared> FindByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return _context.ProductsShared
                 .Where(
                     p => p.Name.ToLower().Contains(Name.ToLower()))
                 .ToList();
            }
            return new List<ProductShared>();
        }
    }
}
