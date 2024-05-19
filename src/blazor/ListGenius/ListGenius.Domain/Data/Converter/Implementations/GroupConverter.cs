using ListGenius.Domain.Data.Converter.Contract;
using ListGenius.Domain.Model;
using ListGenius.Shared.VO.Group;

namespace ListGenius.Domain.Data.Converter.Implementations
{
    public class GroupConverter : IParser<GroupVO, Group>, IParser<Group, GroupVO>
    {
        public Group Parse(GroupVO origin)
        {
            if (origin == null)
            {
                return new Group();
            }
            return new Group
            {
                Id = origin.Id,
                Name = origin.Name,
                Image = origin.Image,
                Enabled = origin.Enabled,
                Description = origin.Description,
                CreatedDate = origin.CreatedDate,
                UpdatedDate = origin.UpdatedDate,
            };
        }

        public List<Group> Parse(List<GroupVO> origin)
        {
            if (origin == null)
            {
                return new List<Group>();
            }
            return origin.Select(Parse).ToList();
        }

        public GroupVO Parse(Group origin)
        {
            if (origin == null)
            {
                return new GroupVO();
            }
            return new GroupVO
            {
                Id = origin.Id,
                Name = origin.Name,
                Image = origin.Image,
                Enabled = origin.Enabled,
                Description = origin.Description,
                CreatedDate = origin.CreatedDate,
                UpdatedDate = origin.UpdatedDate,
            };
        }

        public List<GroupVO> Parse(List<Group> origin)
        {
            if (origin == null)
            {
                return new List<GroupVO>();
            }
            return origin.Select(Parse).ToList();
        }
    }
}
