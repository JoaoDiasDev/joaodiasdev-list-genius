using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Model.Context;
using JoaoDiasDev.ListGenius.Repository.Generic;

namespace JoaoDiasDev.ListGenius.Repository.SharedProductRepo
{
    public class SharedProductRepository : GenericRepository<SharedProduct>, ISharedProductRepository
    {
        public SharedProductRepository(MySQLContext context) : base(context)
        {
        }

        public SharedProduct Disable(long id)
        {
            if (!_context.SharedProducts.Any(p => p.Id.Equals(id)))
            {
                return new SharedProduct();
            }

            var subGroup = _context.SharedProducts.SingleOrDefault(p => p.Id.Equals(id));

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

            return subGroup ?? new SharedProduct();
        }

        public List<SharedProduct> FindByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return _context.SharedProducts
                 .Where(
                     p => p.Name.ToLower().Contains(Name.ToLower()))
                 .ToList();
            }
            return new List<SharedProduct>();
        }
    }
}
