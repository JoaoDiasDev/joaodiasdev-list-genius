using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Model.Context;
using JoaoDiasDev.ListGenius.Repository.Generic;

namespace JoaoDiasDev.ListGenius.Repository.GroupRepo
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(MySQLContext context) : base(context)
        {
        }

        public Group Disable(long id)
        {
            if (!_context.Groups.Any(p => p.Id.Equals(id)))
            {
                return new Group();
            }

            var subGroup = _context.Groups.SingleOrDefault(p => p.Id.Equals(id));

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

            return subGroup ?? new Group();
        }

        public List<Group> FindByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return _context.Groups
                 .Where(
                     p => p.Name.ToLower().Contains(Name.ToLower()))
                 .ToList();
            }
            return new List<Group>();
        }
    }
}
