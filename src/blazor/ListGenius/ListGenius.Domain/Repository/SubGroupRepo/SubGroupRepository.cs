using ListGenius.Domain.Context;
using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.SubGroupRepo
{
    public class SubGroupRepository : GenericRepository<SubGroup>, ISubGroupRepository
    {
        public SubGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public SubGroup Disable(long id)
        {
            if (!_context.SubGroups.Any(p => p.Id.Equals(id)))
            {
                return new SubGroup();
            }

            var subGroup = _context.SubGroups.SingleOrDefault(p => p.Id.Equals(id));

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

            return subGroup ?? new SubGroup();
        }

        public List<SubGroup> FindByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return _context.SubGroups
                 .Where(
                     p => p.Name.ToLower().Contains(Name.ToLower()))
                 .ToList();
            }
            return new List<SubGroup>();
        }
    }
}
