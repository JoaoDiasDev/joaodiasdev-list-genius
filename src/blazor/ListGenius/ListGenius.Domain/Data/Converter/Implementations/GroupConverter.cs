using ListGenius.Domain.Data.Converter.Contract;
using ListGenius.Domain.Model;
using ListGenius.Shared.VO.Group;

namespace ListGenius.Domain.Data.Converter.Implementations
{
    public class GroupConverter : IParser<GroupVO, ProductGroup>, IParser<ProductGroup, GroupVO>
    {
        public ProductGroup Parse(GroupVO origin)
        {
            if (origin == null)
            {
                return new ProductGroup();
            }
            return new ProductGroup
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

        public List<ProductGroup> Parse(List<GroupVO> origin)
        {
            if (origin == null)
            {
                return new List<ProductGroup>();
            }
            return origin.Select(Parse).ToList();
        }

        public GroupVO Parse(ProductGroup origin)
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

        public List<GroupVO> Parse(List<ProductGroup> origin)
        {
            if (origin == null)
            {
                return new List<GroupVO>();
            }
            return origin.Select(Parse).ToList();
        }
    }
}
