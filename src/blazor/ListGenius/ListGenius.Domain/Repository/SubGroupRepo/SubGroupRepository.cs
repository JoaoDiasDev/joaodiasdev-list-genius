using ListGenius.Domain.Context;
using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.SubGroupRepo
{
    public class SubGroupRepository : GenericRepository<ProductSubGroup>, ISubGroupRepository
    {
        public SubGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ProductSubGroup Disable(long id)
        {
            if (!_context.ProductSubGroups.Any(p => p.Id.Equals(id)))
            {
                return new ProductSubGroup();
            }

            var subGroup = _context.ProductSubGroups.SingleOrDefault(p => p.Id.Equals(id));

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

            return subGroup ?? new ProductSubGroup();
        }

        public List<ProductSubGroup> FindByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return _context.ProductSubGroups
                 .Where(
                     p => p.Name.ToLower().Contains(Name.ToLower()))
                 .ToList();
            }
            return new List<ProductSubGroup>();
        }
    }
}
