using ListGenius.Domain.Context;
using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.GroupRepo
{
    public class GroupRepository : GenericRepository<ProductGroup>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ProductGroup Disable(long id)
        {
            if (!_context.ProductGroups.Any(p => p.Id.Equals(id)))
            {
                return new ProductGroup();
            }

            var subGroup = _context.ProductGroups.SingleOrDefault(p => p.Id.Equals(id));

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

            return subGroup ?? new ProductGroup();
        }

        public List<ProductGroup> FindByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return _context.ProductGroups
                 .Where(
                     p => p.Name.ToLower().Contains(Name.ToLower()))
                 .ToList();
            }
            return new List<ProductGroup>();
        }
    }
}
